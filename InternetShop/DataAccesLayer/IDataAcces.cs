using Common;
using System.Collections.Generic;

namespace DataAccesLayer
{
    public interface IDataAcces
    {
        Category[] GetCategories();
        Category GetCategory(int categoryId);
        Product[] GetProducts(int CategoryId);
        Product GetProduct(int productId);
        List<Order> GetOrders();
        List<Order> GetUserOrders(int userId);
        Order GetOrder(int orderId);
        List<User> GetAllUsers();
        User GetUser(int userId);
        int LogIn(string eMail, string password);
        bool AddCategory(string name);
        bool AddProduct(Product product);
        bool AddOrder(int userId, int productId, int productCount);
        bool AddUser(User user);
        bool EditCategory(Category Category);
        bool EditProducts(Product product);
        bool EditUser(User user);
        bool EditOrder(Order order);
        bool DelCategory(Category Category);
        bool DelProducts(Product product);
        bool DelUser(User user);
        bool DelOrder(Order order);
    }
}
