using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication.Repositories
{
    public interface IMapInitializer
    {
        void Register(IMapperConfigurationExpression config);
    }
}
