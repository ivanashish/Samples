using BankApplication.Models;
using BankApplication.Repositories.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApplication.BusinessLogic
{
    public interface ITransactionManager
    {
        Task<IEnumerable<TransactionEntity>> GetTransactions();

        Task<TransactionEntity> GetTransactionByID(int id);

        Task InsertTransaction(TransactionEntity entity);

        Task DeleteTransaction(int id);

        void UpdateTransaction(TransactionEntity entity);

        Task<IEnumerable<TransactionEntity>> GetTransactionsByAccountId(int id);
    }
}
