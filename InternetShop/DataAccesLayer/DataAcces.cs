using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
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
                TryOpenConnection(connection);

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

        public Category GetCategory(int categoryId)
        {
            Category category = new Category();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand();

                command.CommandText = String.Format("SELECT * FROM Category WHERE Id = '{0}'", categoryId);
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    category.CategoryId = (int)reader["Id"];
                    category.Name = (string)reader["Name"];
                }

                reader.Close();
            }
            return category;
        }

        public Product[] GetProducts(int CategoryId)
        {
            List<Product> Products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand();

                command.CommandText = String.Format("SELECT * FROM ProductsView WHERE CategoryId={0}", CategoryId);
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Product product = new Product() { ProductId = (int)reader["Id"], CategoryId = (int)reader["CategoryId"], CategoryName = (string)reader["CategoryName"], Name = (string)reader["Name"], Cost = (double)reader["Cost"], About = (string)reader["About"] };
                        
                        Products.Add(product);
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

        public Product GetProduct(int productId)
        {
            Product product = new Product();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand();

                command.CommandText = String.Format("SELECT * FROM ProductsView WHERE Id={0}", productId);
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    product = new Product() { ProductId = (int)reader["Id"], CategoryId = (int)reader["CategoryId"], CategoryName = (string)reader["CategoryName"], Name = (string)reader["Name"], Cost = (double)reader["Cost"], About = (string)reader["About"] };
                }

                reader.Close();
            }
            return product;
        }

        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand();

                command.CommandText = String.Format("SELECT * FROM OrdersView");
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Order order = new Order();

                        order.OrderId = (int)reader["Id"];
                        order.UserId = (int)reader["UserId"];
                        order.UserName = (string)reader["UserName"];
                        order.ProductId = (int)reader["ProductId"];
                        order.Product = new Product() { ProductId = (int)reader["ProductId"], Name = (string)reader["ProductName"], Cost = (double)reader["ProductCost"], CategoryId = (int)reader["ProductCategory"], About = (string)reader["About"] };
                        order.Count = (int)reader["ProductCount"];
                        order.Status = (string)reader["Status"];

                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        public List<Order> GetUserOrders(int userId)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand();

                command.CommandText = String.Format("SELECT * FROM OrdersView WHERE UserId = '{0}'", userId);
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Order order = new Order();

                        order.OrderId = (int)reader["Id"];
                        order.UserId = (int)reader["UserId"];
                        order.ProductId = (int)reader["ProductId"];
                        order.Product = new Product() { ProductId = (int)reader["ProductId"], Name = (string)reader["ProductName"], Cost = (double)reader["ProductCost"], CategoryId = (int)reader["ProductCategory"], About = (string)reader["About"] };           
                        order.Count = (int)reader["ProductCount"];
                        order.Status = (string)reader["Status"];

                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        public Order GetOrder(int orderId)
        {
            Order order = new Order();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand();

                command.CommandText = String.Format("SELECT * FROM OrdersView WHERE Id = '{0}'", orderId);
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    order.OrderId = (int)reader["Id"];
                    order.UserId = (int)reader["UserId"];
                    order.ProductId = (int)reader["ProductId"];
                    order.Product = new Product() { ProductId = (int)reader["ProductId"], Name = (string)reader["ProductName"], Cost = (double)reader["ProductCost"], CategoryId = (int)reader["ProductCategory"], About = (string)reader["About"] };
                    order.Count = (int)reader["ProductCount"];
                    order.Status = (string)reader["Status"];
                }

                reader.Close();
            }
            return order;
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            string sqlExpression = String.Format("SELECT Id, FName, LName, EMail, Phone, Role FROM Users");

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            User user = new User();

                            user.UserId = (int)reader["Id"];
                            user.FirstName = (string)reader["FName"];
                            user.LastName = (string)reader["LName"];
                            user.EMail = (string)reader["EMail"];
                            user.Phone = (string)reader["Phone"];
                            user.Role = (string)reader["Role"];

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public User GetUser(int userId)
        {
            User user = new User();

            string sqlExpression = String.Format("SELECT Id, FName, LName, EMail, Phone, Role FROM Users WHERE Id = '{0}'", userId);

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
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
            string sqlExpression = String.Format("SELECT * FROM Users WHERE EMail = '{0}' AND Password = '{1}'", eMail, password);

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                TryOpenConnection(connection);

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        if (reader.Read())
                            return (int)reader["Id"];
                        else
                            return -1;
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

        public bool AddOrder(int userId, int productId, int productCount)
        {
            string sqlExpression = String.Format("INSERT INTO Orders (UserId, ProductId, Count) VALUES ('{0}', '{1}','{2}')", userId, productId, productCount);

            return OneCommand(sqlExpression);
        }

        public bool AddUser(User user)
        {
            string sqlExpression;
            if (user.Role != "" & user.Role != null)
            {
                sqlExpression = String.Format("INSERT INTO Users (FName, LName, EMail, Password, Phone, Role) VALUES ('{0}', '{1}','{2}', '{3}','{4}','{5}')", user.FirstName, user.LastName, user.EMail, user.Password, user.Phone, user.Role);
            }
            else
            {
                sqlExpression = String.Format("INSERT INTO Users (FName, LName, EMail, Password, Phone) VALUES ('{0}', '{1}','{2}', '{3}','{4}')", user.FirstName, user.LastName, user.EMail, user.Password, user.Phone);
            }
            return OneCommand(sqlExpression);
        }
                
        public bool EditCategory(Category category)
        {
            string sqlExpression = String.Format("UPDATE Category SET Name = '{1}' FROM Category WHERE Id={0}", category.CategoryId, category.Name);

            return OneCommand(sqlExpression);
        }

        public bool EditProducts(Product product)
        {
            string sqlExpression = String.Format("UPDATE Products SET Name = '{1}', Cost = '{2}', About = '{3}' FROM Products WHERE Id={0}", product.ProductId, product.Name,  product.Cost, product.About);

            return OneCommand(sqlExpression);
        }

        public bool EditUser(User user)
        {
            string sqlExpression = String.Format("UPDATE Users SET FirstName = '{1}', LastName = '{2}', EMail = '{3}', Password = '{4}', Phone = '{5}', Role = '{6}' FROM Users WHERE Id={0}", user.UserId, user.FirstName, user.LastName, user.EMail, user.Password, user.Phone, user.Role);

            return OneCommand(sqlExpression);
        }

        public bool EditOrder(Order order)
        {
            string sqlExpression = String.Format("UPDATE Orders SET ProductId = '{1}', Count = '{2}', Status = '{3}' FROM Orders WHERE Id={0}", order.OrderId, order.ProductId, order.Count, order.Status);

            return OneCommand(sqlExpression);
        }

        public bool DelCategory(Category category)
        {
            string sqlExpression = String.Format("DELETE Category WHERE Id={0}", category.CategoryId);

            return OneCommand(sqlExpression);
        }

        public bool DelProducts(Product product)
        {
            string sqlExpression = String.Format("DELETE Products WHERE Id={0}", product.ProductId);

            return OneCommand(sqlExpression);
        }

        public bool DelUser(User user)
        {
            string sqlExpression = String.Format("DELETE Users WHERE Id={0}", user.UserId);

            return OneCommand(sqlExpression);
        }

        public bool DelOrder(Order order)
        {
            string sqlExpression = String.Format("DELETE Orders WHERE Id={0}", order.OrderId);

            return OneCommand(sqlExpression);
        }

        private void TryOpenConnection(SqlConnection connection)
        {
            try
            {
                connection.Open();
            }
            catch (SqlException)
            {
                connection.Close();
                TryOpenConnection(connection);
            }
        }

        private bool OneCommand(string sqlExpression)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    TryOpenConnection(connection);

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
