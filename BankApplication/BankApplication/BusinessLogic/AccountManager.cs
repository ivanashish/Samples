using System.Collections.Generic;
using System.Threading.Tasks;
using BankApplication.CustomExceptions;
using BankApplication.Models;
using BankApplication.Repositories;

namespace BankApplication.BusinessLogic
{
    public class AccountManager : IAccountManager
    {
        private IAccountRepository accountRepository;

        public AccountManager(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task DeleteAccount(int id)
        {
            await this.accountRepository.DeleteAccount(id);
        }

        public async Task<AccountEntity> GetAccountByID(int id)
        {
            var account = await this.accountRepository.GetAccountByID(id);

            if (account == null)
            {
                throw new BusinessException("Record not found");
            }

            return account;
        }

        public async Task<IEnumerable<AccountEntity>> GetAccounts()
        {
            return await this.accountRepository.GetAccounts();
        }

        public async Task InsertAccount(AccountEntity entity)
        {
            await this.accountRepository.InsertAccount(entity);
        }

        public void UpdateAccount(AccountEntity entity)
        {
            this.accountRepository.UpdateAccount(entity);
        }

        public async Task<IEnumerable<AccountEntity>> GetAccountsByBranchAndCustomerIds(int branchId, int customerId)
        {
            return await this.accountRepository.GetAccountsByBranchAndCustomerIds(branchId, customerId);
        }
    }
}
