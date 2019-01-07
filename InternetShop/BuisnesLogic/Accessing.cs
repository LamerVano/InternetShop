using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccesLayer;
using StructureMap;

namespace BuisnesLogic
{
    public class Accessing: IAccessing
    {
        
        public IDataAcces DataAcces { get; set; }

        public Accessing(IDataAcces dataAcces)
        {
            DataAcces = dataAcces;
        }

        public Category[] GetAllCategories()
        {
            return DataAcces.GetCategories();
        }

        public Product[] GetProducts(int CategoryId)
        {
            return DataAcces.GetProducts(CategoryId);
        }

        public bool AddCategory(string name)
        {
            return DataAcces.AddCategory(name);
        }

        public bool AddProducts(Product product)
        {
            return DataAcces.AddProduct(product);
        }

        public Busket GetBusket(int userId)
        {
            return DataAcces.GetBusket(userId);
        }

        public User GetUser(int userId)
        {
            return DataAcces.GetUser(userId);
        }

        public bool AddUser(User user)
        {
            return DataAcces.AddUser(user);
        }

        public bool AddInBusket(int userId, int productId, int productCount)
        {
            return DataAcces.AddInBusket(userId, productId, productCount);
        }

        public int LogIn(string eMail, string password)
        {
            return DataAcces.LogIn(eMail, password);
        }
    }
}
