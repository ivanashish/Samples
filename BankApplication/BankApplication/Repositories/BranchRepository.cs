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
    public class BranchRepository : IBranchRepository
    {
        private ApplicationDbContext context;
        private bool disposed = false;

        public BranchRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task DeleteBranch(int id)
        {
            var entity = await context.Branches.FindAsync(id);
            context.Branches.Remove(entity);
        }

        public async Task<BranchEntity> GetBranchByID(int id)
        {
            return Mapper.Map<BranchEntity>(await context.Branches.FindAsync(id));
        }

        public async Task<IEnumerable<BranchEntity>> GetBranches()
        {
            return Mapper.Map<IEnumerable<BranchEntity>>(await context.Branches.ToListAsync());
        }

        public async Task InsertBranch(BranchEntity entity)
        {
            await context.Branches.AddAsync(Mapper.Map<Branch>(entity));
        }

        public void UpdateBranch(BranchEntity entity)
        {
            context.Entry(Mapper.Map<Branch>(entity)).State = EntityState.Modified;
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
