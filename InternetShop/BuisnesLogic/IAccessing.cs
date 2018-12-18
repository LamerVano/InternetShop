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
        bool AddCategory(string name);
        bool AddProducts(Product product);
    }
}
