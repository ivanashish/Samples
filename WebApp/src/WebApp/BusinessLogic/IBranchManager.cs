using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.BusinessLogic
{
    public interface IBranchManager
    {
        Task<IEnumerable<BranchEntity>> GetBranches();

        Task<BranchEntity> GetBranchByID(int id);

        void InsertBranch(BranchEntity entity);

        Task DeleteBranch(int id);

        void UpdateBranch(BranchEntity entity);
    }
}
