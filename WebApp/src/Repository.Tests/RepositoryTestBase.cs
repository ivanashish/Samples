using WebApp.Repository;

namespace Repository.Tests
{
    public class RepositoryTestBase
    {
        protected ApplicationDbContext Context { get; private set; }

        protected IBranchRepository BranchRepository { get; private set; }

        public RepositoryTestBase()
        {
            MapConfig.Initialize();
            this.Context = WebAppContextSeedExtensions.GetInMemoryDbContext();
            this.BranchRepository = new BranchRepository(this.Context);
        }
    }
}
