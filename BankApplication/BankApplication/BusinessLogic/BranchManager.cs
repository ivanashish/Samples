using System.Collections.Generic;
using System.Threading.Tasks;
using BankApplication.CustomExceptions;
using BankApplication.Models;
using BankApplication.Repositories;

namespace BankApplication.BusinessLogic
{
    public class BranchManager : IBranchManager
    {
        private IBranchRepository branchRepository;

        public BranchManager(IBranchRepository branchRepository)
        {
            this.branchRepository = branchRepository;
        }

        public async Task DeleteBranch(int id)
        {
            await this.branchRepository.DeleteBranch(id);
        }

        public async Task<BranchEntity> GetBranchByID(int id)
        {
            var branch = await this.branchRepository.GetBranchByID(id);

            if (branch == null)
            {
                throw new BusinessException("Record not found");
            }

            return branch;
        }

        public async Task<IEnumerable<BranchEntity>> GetBranches()
        {
            return await this.branchRepository.GetBranches();
        }

        public async Task InsertBranch(BranchEntity entity)
        {
            await this.branchRepository.InsertBranch(entity);
        }

        public void UpdateBranch(BranchEntity entity)
        {
            this.branchRepository.UpdateBranch(entity);
        }
    }
}
