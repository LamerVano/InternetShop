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

        public bool AddCategory(string name)
        {
            string sqlExpression = String.Format("INSERT INTO Category (Name) VALUES ('{0}')", name);

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

        public bool AddProduct(Product product)
        {
            string sqlExpression = String.Format("INSERT INTO Products (CategoryId, Name, Cost, About) VALUES ('{0}', '{1}','{2}','{3}')", product.CategoryId, product.Name, product.Cost, product.About );

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
