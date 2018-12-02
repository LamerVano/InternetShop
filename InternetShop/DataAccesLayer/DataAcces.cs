using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class DataAcces: IDataAcces
    {
        public Category[] GetCategories()
        {
            return new Category[] { new Category()};
        }
    }
}
