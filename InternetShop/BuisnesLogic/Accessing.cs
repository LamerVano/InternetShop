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

        public Product GetProduct(int ProductId)
        {
            return DataAcces.GetProduct(ProductId);
        }

        public bool AddCategory(string name)
        {
            return DataAcces.AddCategory(name);
        }

        public bool AddProducts(Product product)
        {
            return DataAcces.AddProduct(product);
        }

        public List<Order> GetOrders()
        {
            return DataAcces.GetOrders();
        }

        public List<Order> GetUserOrders(int userId)
        {
            return DataAcces.GetUserOrders(userId);
        }

        public User GetUser(int userId)
        {
            return DataAcces.GetUser(userId);
        }

        public bool AddUser(User user)
        {
            return DataAcces.AddUser(user);
        }

        public bool AddOrder(int userId, int productId, int productCount)
        {
            return DataAcces.AddOrder(userId, productId, productCount);
        }

        public int LogIn(string eMail, string password)
        {
            return DataAcces.LogIn(eMail, password);
        }

        public bool EditCategory(Category category)
        {
            return DataAcces.EditCategory(category);
        }

        public bool EditProducts(Product product)
        {
            return DataAcces.EditProducts(product);
        }

        public bool EditUser(User user)
        {
            return DataAcces.EditUser(user);
        }

        public bool EditOrder(Order order)
        {
            return DataAcces.EditOrder(order);
        }

        public bool DelCategory(Category category)
        {
            return DataAcces.DelCategory(category);
        }

        public bool DelProducts(Product product)
        {
            return DataAcces.DelProducts(product);
        }

        public bool DelUser(User user)
        {
            return DataAcces.DelUser(user);
        }

        public bool DelOrder(Order order)
        {
            return DataAcces.DelOrder(order);
        }

        public Category GetCategory(int categoryId)
        {
            return DataAcces.GetCategory(categoryId);
        }

        public Order GetOrder(int orderId)
        {
            return DataAcces.GetOrder(orderId);
        }

        public List<User> GetAllUsers()
        {
            return DataAcces.GetAllUsers();
        }
    }
}
