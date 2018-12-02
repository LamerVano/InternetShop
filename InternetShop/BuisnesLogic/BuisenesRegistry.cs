using DataAccesLayer;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogic
{
    public class BuisenesRegistry: Registry
    {
        public BuisenesRegistry()
        {
            //Scan(
            //    scan =>
            //    {
            //        scan.TheCallingAssembly();
            //    });

            For<IDataAcces>().Use<DataAcces>();
            Forward<IDataAcces, DataAcces>();
            For<IAccessing>().Use<Accessing>();
        }
    }
}
