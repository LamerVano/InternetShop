using Common;
using System.Collections.Generic;

namespace BuisnesLogic
{
    public interface IAccessing
    {
        Category[] GetAllCategories();
        Category GetCategory(int categoryId);
        int GetCategoryId(int productId);
        Product[] GetProducts(int categoryId);
        Product GetProduct(int productId);
        List<Order> GetOrders();
        List<Order> GetUserOrders(int userId);
        Order GetOrder(int orderId);
        List<User> GetAllUsers();
        User GetUser(int userId);
        int LogIn(string eMail, string password);
        List<IModel> FindAll(string str);
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
