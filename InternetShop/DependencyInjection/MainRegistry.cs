using BuisnesLogic;
using DataAccesLayer;
using StructureMap;

namespace DependencyInjection
{
    public class MainRegistry: Registry
    {
        public MainRegistry()
        {
            Scan(
                scan => {
                    scan.WithDefaultConventions();
                    scan.AssemblyContainingType<Accessing>();
                    scan.AssemblyContainingType<DataAcces>();
                    scan.LookForRegistries();
                });

        }

    }
}
