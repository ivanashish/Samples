using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BankApplication.Models;
using BankApplication.Repositories.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BankApplication.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private ApplicationDbContext context;
        private bool disposed = false;

        public AccountRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task DeleteAccount(int id)
        {
            var entity = await context.Accounts.FindAsync(id);
            context.Accounts.Remove(entity);
        }

        public async Task<AccountEntity> GetAccountByID(int id)
        {
            return Mapper.Map<AccountEntity>(await context.Accounts.Include(a => a.Branch).Include(a => a.Customer).SingleOrDefaultAsync(x => x.Id == id));
        }

        public async Task<IEnumerable<AccountEntity>> GetAccounts()
        {
            return Mapper.Map<IEnumerable<AccountEntity>>(await context.Accounts.Include(a => a.Branch).Include(a => a.Customer).ToListAsync());
        }

        public async Task InsertAccount(AccountEntity entity)
        {
            await context.Accounts.AddAsync(Mapper.Map<Account>(entity));
        }

        public void UpdateAccount(AccountEntity entity)
        {
            context.Entry(Mapper.Map<Account>(entity)).State = EntityState.Modified;
        }

        public async Task<IEnumerable<AccountEntity>> GetAccountsByBranchAndCustomerIds(int branchId, int customerId)
        {
            return Mapper.Map<IEnumerable<AccountEntity>>(await (from acc in context.Accounts
                                                                  join cust in context.Customers on acc.CustomerId equals cust.Id
                                                                  join branch in context.Branches on acc.BranchId equals branch.Id
                                                                  where branch.Id == branchId && cust.Id == customerId
                                                                  select acc).Distinct().ToListAsync());

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
    }
}
