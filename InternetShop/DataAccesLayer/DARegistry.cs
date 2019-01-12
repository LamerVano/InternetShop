using StructureMap;

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
