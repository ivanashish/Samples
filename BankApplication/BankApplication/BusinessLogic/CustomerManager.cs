using System.Collections.Generic;
using System.Threading.Tasks;
using BankApplication.CustomExceptions;
using BankApplication.Models;
using BankApplication.Repositories;

namespace BankApplication.BusinessLogic
{
    public class CustomerManager : ICustomerManager
    {
        private ICustomerRepository customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task DeleteCustomer(int id)
        {
            await this.DeleteCustomer(id);
        }

        public async Task<CustomerEntity> GetCustomerByID(int id)
        {
            var customer = await this.customerRepository.GetCustomerByID(id);

            if (customer == null)
            {
                throw new BusinessException("Record not found");
            }

            return customer;
        }

        public async Task<IEnumerable<CustomerEntity>> GetCustomers()
        {
            return await this.customerRepository.GetCustomers();
        }

        public async Task<IEnumerable<CustomerEntity>> GetCustomersByBranchId(int id)
        {
            return await this.customerRepository.GetCustomersByBranchId(id);
        }

        public async Task InsertCustomer(CustomerEntity entity)
        {
            await this.customerRepository.InsertCustomer(entity);
        }

        public void UpdateCustomer(CustomerEntity entity)
        {
            this.customerRepository.UpdateCustomer(entity);
        }
    }
}
