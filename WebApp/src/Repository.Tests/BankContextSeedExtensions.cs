using Microsoft.EntityFrameworkCore;
using System;
using WebApp.Repository;
using WebApp.Repository.DbModels;

namespace Repository.Tests
{
    public static class WebAppContextSeedExtensions
    {
        public static ApplicationDbContext GetInMemoryDbContext()
        {
            var optionDef = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionDef.UseInMemoryDatabase(DateTime.Now.Ticks.ToString());

            var appDbContext = new ApplicationDbContext(optionDef.Options);
            appDbContext.SeedBranches();
            return appDbContext;
        }

        public async static void SeedBranches(this ApplicationDbContext applicationDbContext)
        {
            if(!await applicationDbContext.Branches.AnyAsync())
            {
                applicationDbContext.Branches.Add(new Branch { Id = 123 });
                applicationDbContext.Branches.Add(new Branch { Id = 234 });
                applicationDbContext.Branches.Add(new Branch { Id = 345 });

                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
