using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBanking.DataAccess.Model.Repository
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetTransactionAsync(int id);
        IEnumerable<Transaction> GetAllTransactions(int AccountId);
        Task<Transaction> AddAsync(Transaction transaction);
        Task<Transaction> UpdateAsync(Transaction transaction);
        Task<Transaction> DeleteAsync(int id);
        Task<Transaction> GetLatestTransactionAsync(int accountId);

    }
}
