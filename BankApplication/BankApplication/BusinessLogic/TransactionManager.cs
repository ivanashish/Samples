using System.Collections.Generic;
using System.Threading.Tasks;
using BankApplication.CustomExceptions;
using BankApplication.Models;
using BankApplication.Repositories;

namespace BankApplication.BusinessLogic
{
    public class TransactionManager : ITransactionManager
    {
        private ITransactionRepository transactionRepository;

        public TransactionManager(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public async Task DeleteTransaction(int id)
        {
            await this.transactionRepository.DeleteTransaction(id);
        }

        public async Task<TransactionEntity> GetTransactionByID(int id)
        {
            var transaction = await this.transactionRepository.GetTransactionByID(id);

            if (transaction == null)
            {
                throw new BusinessException("Record not found");
            }

            return transaction;
        }

        public async Task<IEnumerable<TransactionEntity>> GetTransactions()
        {
            return await this.transactionRepository.GetTransactions();
        }

        public async Task InsertTransaction(TransactionEntity entity)
        {
            await this.transactionRepository.InsertTransaction(entity);
        }

        public void UpdateTransaction(TransactionEntity entity)
        {
            this.transactionRepository.UpdateTransaction(entity);
        }

        public async Task<IEnumerable<TransactionEntity>> GetTransactionsByAccountId(int id)
        {
            return await this.transactionRepository.GetTransactionsByAccountId(id);
        }
    }
}
