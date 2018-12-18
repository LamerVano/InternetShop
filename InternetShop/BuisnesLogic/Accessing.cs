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
    }
}
