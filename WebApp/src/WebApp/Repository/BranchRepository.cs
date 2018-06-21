using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApp.Models;
using WebApp.Repository.DbModels;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Repository
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
            var entity = await context.Branches.SingleAsync(x => x.Id == id);
            context.Branches.Remove(entity);
        }

        public async Task<BranchEntity> GetBranchByID(int id)
        {
            return Mapper.Map<BranchEntity>(await context.Branches.SingleAsync(x => x.Id == id));
        }

        public async Task<IEnumerable<BranchEntity>> GetBranches()
        {
            return Mapper.Map<IEnumerable<BranchEntity>>(await context.Branches.ToListAsync());
        }

        public void InsertBranch(BranchEntity entity)
        {
            context.Branches.Add(Mapper.Map<Branch>(entity));
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
