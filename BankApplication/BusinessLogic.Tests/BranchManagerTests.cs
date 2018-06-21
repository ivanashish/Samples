using BankApplication.BusinessLogic;
using BankApplication.Models;
using BankApplication.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogic.Tests
{
    public class BranchManagerTests
    {
        private readonly Mock<IBranchRepository> mockBranchRepository = new Mock<IBranchRepository>();
        private IBranchManager branchManager;

        public BranchManagerTests()
        {
            this.branchManager = new BranchManager(this.mockBranchRepository.Object);
        }

        [Fact]
        public async Task GetBranchesReturnsListOfBranches()
        {
            var branches = new List<BranchEntity> { new BranchEntity { Id = 111 } };

            this.mockBranchRepository.Setup(x => x.GetBranches()).ReturnsAsync(branches);

            var result = await this.branchManager.GetBranches();

            Assert.Equal(branches, result);
        }
    }
}
