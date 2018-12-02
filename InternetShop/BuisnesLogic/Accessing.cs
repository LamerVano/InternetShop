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
        public Category[] GetCategories()
        {
            return this.DataAcces.GetCategories();
        }
    }
}
