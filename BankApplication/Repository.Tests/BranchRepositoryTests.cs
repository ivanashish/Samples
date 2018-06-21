using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Repository.Tests
{
    public class BranchRepositoryTests : RepositoryTestBase
    {

        [Fact]
        public async Task GetBranchesReturnsListOfBranches()
        {
            var branches = await this.BranchRepository.GetBranches();
            Assert.Equal(3, branches.Count());
        }
    }
}
