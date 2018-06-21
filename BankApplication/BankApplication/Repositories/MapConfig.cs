using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BankApplication.Repositories
{
    public static class MapConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                var currentAssembly = Assembly.GetExecutingAssembly();
                var interfaceType = typeof(IMapInitializer);
                var mapInitialzers = currentAssembly.GetTypes().Where(t => interfaceType != t && interfaceType.IsAssignableFrom(t));
                foreach (var mapInitialzer in mapInitialzers)
                {
                    var initializer = (IMapInitializer)Activator.CreateInstance(mapInitialzer);
                    initializer.Register(config);
                }
            });
        }
    }
}
