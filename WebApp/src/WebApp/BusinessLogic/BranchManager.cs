using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.CustomExceptions;
using WebApp.Models;
using WebApp.Repository;

namespace WebApp.BusinessLogic
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

        public void InsertBranch(BranchEntity entity)
        {
            this.branchRepository.InsertBranch(entity);
        }

        public void UpdateBranch(BranchEntity entity)
        {
            this.branchRepository.UpdateBranch(entity);
        }
    }
}
