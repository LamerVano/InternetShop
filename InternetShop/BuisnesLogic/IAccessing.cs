using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogic
{
    public interface IAccessing
    {
        Category[] GetAllCategories();
        Category GetCategory(int categoryId);
        Product[] GetProducts(int categoryId);
        Product GetProduct(int productId);
        List<Order> GetUserOrders(int userId);
        Order GetOrder(int orderId);
        List<User> GetAllUsers();
        User GetUser(int userId);
        int LogIn(string eMail, string password);
        bool AddCategory(string name);
        bool AddProducts(Product product);
        bool AddUser(User user);
        bool AddOrder(int userId, int productId, int productCount);
        bool EditCategory(Category Category);
        bool EditProducts(Product product);
        bool EditUser(User user);
        bool EditOrder(Order order);
        bool DelCategory(Category newCategory);
        bool DelProducts(Product product);
        bool DelUser(User user);
        bool DelOrder(Order order);
    }
}
