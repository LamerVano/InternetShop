using DataAccesLayer;
using StructureMap;

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
