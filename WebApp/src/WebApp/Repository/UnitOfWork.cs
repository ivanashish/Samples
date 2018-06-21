using System;
using System.Threading.Tasks;

namespace WebApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext context { get; set; }

        private bool disposed = false;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Commit()
        {
            var result = await this.context.SaveChangesAsync();

            ////if(this.context.Database.CurrentTransaction != null)
            ////{
            ////    this.context.Database.CommitTransaction();
            ////}

            return result;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposed)
            {
                return;
            }

            if(disposing)
            {
                this.context?.Dispose();
            }

            this.disposed = true;
        }
    }
}
