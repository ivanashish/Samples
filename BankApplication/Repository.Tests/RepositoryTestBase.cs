using BankApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Tests
{
    public class RepositoryTestBase
    {
        protected ApplicationDbContext Context { get; private set; }

        protected IBranchRepository BranchRepository { get; private set; }

        public RepositoryTestBase()
        {
            MapConfig.Initialize();
            this.Context = BankContextSeedExtensions.GetInMemoryDbContext();
            this.BranchRepository = new BranchRepository(this.Context);
        }
    }
}
