using BankApplication.Models;
using BankApplication.Repositories.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication.BusinessLogic
{
    public interface IBranchManager
    {
        Task<IEnumerable<BranchEntity>> GetBranches();

        Task<BranchEntity> GetBranchByID(int id);

        Task InsertBranch(BranchEntity entity);

        Task DeleteBranch(int id);

        void UpdateBranch(BranchEntity entity);
    }
}
