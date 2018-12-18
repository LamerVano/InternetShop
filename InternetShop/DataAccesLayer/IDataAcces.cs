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
        bool AddCategory(string name);
        bool AddProduct(Product product);
    }
}
