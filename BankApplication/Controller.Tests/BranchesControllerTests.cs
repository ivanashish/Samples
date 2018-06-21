using BankApplication.BusinessLogic;
using BankApplication.Controllers;
using BankApplication.Models;
using BankApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Controller.Tests
{
    public class BranchesControllerTests : IDisposable
    {
        private readonly BranchesController branchesController;
        private readonly Mock<IBranchManager> mockBranchManager = new Mock<IBranchManager>();
        private readonly Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
        private bool disposed = false;

        public BranchesControllerTests()
        {
            this.branchesController = new BranchesController(this.mockBranchManager.Object, this.mockUnitOfWork.Object);
        }

        [Fact]
        public async Task IndexReturnsViewWithListOfBranches()
        {
            var branches = new List<BranchEntity> { new BranchEntity { Id = 123 } };
            this.mockBranchManager.Setup(x => x.GetBranches()).ReturnsAsync(branches);

            var result = await this.branchesController.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(result);
            Assert.NotNull(viewResult);
            Assert.Equal(branches, viewResult.Model);
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
                this.branchesController?.Dispose();
            }

            this.disposed = true;
        }
    }
}
