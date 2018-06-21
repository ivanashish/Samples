using BankApplication.Models;
using BankApplication.Repositories.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication.BusinessLogic
{
    public interface IAccountManager
    {
        Task<IEnumerable<AccountEntity>> GetAccounts();

        Task<AccountEntity> GetAccountByID(int id);

        Task InsertAccount(AccountEntity entity);

        Task DeleteAccount(int id);

        void UpdateAccount(AccountEntity entity);

        Task<IEnumerable<AccountEntity>> GetAccountsByBranchAndCustomerIds(int branchId, int customerId);
    }
}
