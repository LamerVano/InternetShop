using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public interface IDataAcces
    {
        Category[] GetCategories();
        Product[] GetProducts(int CategoryId);
        Busket GetBusket(int userId);
        User GetUser(int userId);
        int LogIn(string eMail, string password);
        bool AddCategory(string name);
        bool AddProduct(Product product);
        bool AddInBusket(int userId, int productId, int productCount);
        bool AddUser(User user);
    }
}
