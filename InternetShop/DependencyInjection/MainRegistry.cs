using BuisnesLogic;
using DataAccesLayer;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
