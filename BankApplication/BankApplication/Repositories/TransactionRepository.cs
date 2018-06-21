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
    public class TransactionRepository : ITransactionRepository
    {
        private ApplicationDbContext context;
        private bool disposed = false;

        public TransactionRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task DeleteTransaction(int id)
        {
            var entity = await context.Transactions.FindAsync(id);
            context.Transactions.Remove(entity);
        }

        public async Task<TransactionEntity> GetTransactionByID(int id)
        {
            return Mapper.Map<TransactionEntity>(await context.Transactions.Include(t => t.Account).SingleOrDefaultAsync(x => x.Id == id));
        }

        public async Task<IEnumerable<TransactionEntity>> GetTransactions()
        {
            return Mapper.Map<IEnumerable<TransactionEntity>>(await context.Transactions.Include(t => t.Account).ToListAsync());
        }

        public async Task InsertTransaction(TransactionEntity entity)
        {
            await context.Transactions.AddAsync(Mapper.Map<Transaction>(entity));
        }

        public void UpdateTransaction(TransactionEntity entity)
        {
            context.Entry(Mapper.Map<Transaction>(entity)).State = EntityState.Modified;
        }

        public async Task<IEnumerable<TransactionEntity>> GetTransactionsByAccountId(int id)
        {
            return Mapper.Map<IEnumerable<TransactionEntity>>(await context.Transactions.Where(x => x.AccountId == id).ToListAsync());
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
