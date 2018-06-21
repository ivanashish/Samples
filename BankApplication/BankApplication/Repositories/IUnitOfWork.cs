using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit();
    }
}
