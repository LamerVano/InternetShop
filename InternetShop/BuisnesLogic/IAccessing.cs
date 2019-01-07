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
        Product[] GetProducts(int CategoryId);
        Busket GetBusket(int userId);
        User GetUser(int userId);
        int LogIn(string eMail, string password);
        bool AddCategory(string name);
        bool AddProducts(Product product);
        bool AddUser(User user);
        bool AddInBusket(int userId, int productId, int productCount);
    }
}
