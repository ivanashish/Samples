using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public interface IMapInitializer
    {
        void Register(IMapperConfigurationExpression config);
    }
}
