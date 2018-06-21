using BankApplication.Models;
using BankApplication.Repositories.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication.Repositories
{
    public interface ICustomerRepository : IDisposable
    {
        Task<IEnumerable<CustomerEntity>> GetCustomers();

        Task<CustomerEntity> GetCustomerByID(int id);

        Task InsertCustomer(CustomerEntity entity);

        Task DeleteCustomer(int id);

        void UpdateCustomer(CustomerEntity entity);

        Task<IEnumerable<CustomerEntity>> GetCustomersByBranchId(int id);
    }
}
