using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class DARegistry: Registry
    {
        public DARegistry()
        {
            //Scan(
            //    scan =>
            //    {
            //        scan.TheCallingAssembly();
            //    });

            For<IDataAcces>().Use<DataAcces>();
            
        }
    }
}
