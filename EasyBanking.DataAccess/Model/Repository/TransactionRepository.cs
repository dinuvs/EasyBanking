using EasyBanking.DataAccess.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBanking.DataAccess.Model.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            _context.Transaction.Add(transaction);
            var result=await _context.SaveChangesAsync();
            if(result > 0)
            {
                return transaction;
            }
            return null;
        }

        public async Task<Transaction> DeleteAsync(int id)
        {

            var transaction = _context.Transaction.Find(id);
            if (transaction != null)
            {
                _context.Remove(transaction);
                await _context.SaveChangesAsync();
            }
            return transaction;
        }

        public IEnumerable<Transaction> GetAllTransactions(int AccountId)
        {
            var transactions = _context.Transaction.Include("Account").ToList();
            transactions = _context.Transaction.Where(e => e.AccountId == AccountId).OrderByDescending(t=>t.LastUpdated).ToList();
            return transactions;
        }

        public async Task<Transaction> GetTransactionAsync(int id)
        {
            return await _context.Transaction.FindAsync(id);
        }

        public async Task<Transaction> GetLatestTransactionAsync(int accountId)
        {
            return await _context.Transaction.OrderByDescending(t => t.TransactionId).FirstAsync(t => t.AccountId == accountId);
        }

        public async Task<Transaction> UpdateAsync(Transaction transaction)
        {
            var transactionChanges = _context.Transaction.Attach(transaction);
            transactionChanges.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return transaction;
        }

       

    }
}
