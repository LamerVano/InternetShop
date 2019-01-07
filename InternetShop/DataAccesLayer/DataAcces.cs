using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class DataAcces: IDataAcces
    {
        const string CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Database;Integrated Security=True";
        public Category[] GetCategories()
        {
            List<Category> Categories = new List<Category>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = "SELECT * FROM Category";
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Categories.Add(new Category() { CategoryId = reader.GetInt32(0), Name = reader.GetString(1) });
                    }
                }

                reader.Close();
            }
            
            if(Categories.Count != 0)
            {
                return Categories.ToArray();
            }
            else
            {
                return new Category[] { new Category() };
            }
        }

        public Product[] GetProducts(int CategoryId)
        {
            List<Product> Products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = String.Format("SELECT * FROM Products WHERE CategoryId={0}", CategoryId);
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Products.Add(new Product() { ProductId = reader.GetInt32(0), CategoryId = reader.GetInt32(1), Name = reader.GetString(2), Cost = reader.GetDecimal(3), About = reader.GetString(4) });
                    }
                }

                reader.Close();
            }

            if (Products.Count != 0)
            {
                return Products.ToArray();
            }
            else
            {
                return new Product[] { new Product() };
            }
        }

        public Busket GetBusket(int UserId)
        {
            Busket busket = new Busket();

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("Busket", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter userParam = new SqlParameter
                {
                    ParameterName = "@userId",
                    Value = UserId
                };

                command.Parameters.Add(userParam);

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        busket.BasketId.Add((int)reader["Id"]);
                        busket.Products.Add(new Product() { ProductId = (int)reader["ProductId"], Name = (string)reader["ProductName"], Cost = (int)reader["ProductCost"], CategoryId = (int)reader["ProductCategory"], About = (string)reader["About"] });
                        busket.Counts.Add((int)reader["ProductCount"]);
                    }
                }
            }

            return busket;
        }

        public User GetUser(int userId)
        {
            User user = new User();

            string sqlExpression = String.Format("SELECT Id, FName, LName, EMail, Phone, Password FROM Users WHERE Id = userId");

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        user.UserId = (int)reader["Id"];
                        user.FirstName = (string)reader["FName"];
                        user.LastName = (string)reader["LName"];
                        user.EMail = (string)reader["EMail"];
                        user.Phone = (string)reader["Phone"];
                        user.Role = (string)reader["Role"];
                    }
                }
            }

            return user;
        }

        public int LogIn(string eMail, string password)
        {
            string sqlExpression = String.Format("SELECT * FROM Users WHERE EMail = {0} AND Password = {1}", eMail, password);

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        return (int)reader["Id"];
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public bool AddCategory(string name)
        {
            string sqlExpression = String.Format("INSERT INTO Category (Name) VALUES ('{0}')", name);

            return OneCommand(sqlExpression);
        }

        public bool AddProduct(Product product)
        {
            string sqlExpression = String.Format("INSERT INTO Products (CategoryId, Name, Cost, About) VALUES ('{0}', '{1}','{2}','{3}')", product.CategoryId, product.Name, product.Cost, product.About );

            return OneCommand(sqlExpression);
        }

        public bool AddInBusket(int userId, int productId, int productCount)
        {
            string sqlExpression = String.Format("INSERT INTO Buskets (UserId, ProductId, Count) VALUES ('{0}', '{1}','{2}')", userId, productId, productCount);

            return OneCommand(sqlExpression);
        }

        public bool AddUser(User user)
        {
            string sqlExpression = String.Format("INSERT INTO Users (Id, FName, LName, EMail, Password, Phone, Role) VALUES ('{0}', '{1}','{2}', '{3}','{4}','{5}','{6}')", user.UserId, user.FirstName, user.LastName, user.EMail, user.Password, user.Phone, user.Role);

            return OneCommand(sqlExpression);
        }


        private bool OneCommand(string sqlExpression)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
