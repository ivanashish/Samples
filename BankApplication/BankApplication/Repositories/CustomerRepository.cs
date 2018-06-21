using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankApplication.Models;
using BankApplication.Repositories.DbModels;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext context;
        private bool disposed = false;

        public CustomerRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task DeleteCustomer(int id)
        {
            var entity = await context.Customers.FindAsync(id);
            context.Customers.Remove(entity);
        }

        public async Task<CustomerEntity> GetCustomerByID(int id)
        {
            return Mapper.Map<CustomerEntity>(await context.Customers.FindAsync(id));
        }

        public async Task<IEnumerable<CustomerEntity>> GetCustomers()
        {
            return Mapper.Map<IEnumerable<CustomerEntity>>(await context.Customers.ToListAsync());
        }

        public async Task InsertCustomer(CustomerEntity entity)
        {
            await context.Customers.AddAsync(Mapper.Map<Customer>(entity));
        }

        public void UpdateCustomer(CustomerEntity entity)
        {
            context.Entry(Mapper.Map<Customer>(entity)).State = EntityState.Modified;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                this.context?.Dispose();
            }

            this.disposed = true;
        }

        public async Task<IEnumerable<CustomerEntity>> GetCustomersByBranchId(int id)
        {
            return Mapper.Map<IEnumerable<CustomerEntity>>(await (from cust in context.Customers
                                                                 join acc in context.Accounts on cust.Id equals acc.CustomerId
                                                                 join branch in context.Branches on acc.BranchId equals branch.Id
                                                                 where branch.Id == id
                                                                 select cust).Distinct().ToListAsync());
        }
    }
}
