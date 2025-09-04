using FinanceManager.App.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManager.App.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllAsync(DateTime? from = null, DateTime? to = null);
        Task<Transaction> AddAsync(Transaction t);
        Task UpdateAsync(Transaction t);
        Task DeleteAsync(Transaction t);
        Task<decimal> GetIncomeSumAsync(DateTime? from = null, DateTime? to = null);
        Task<decimal> GetExpenseSumAsync(DateTime? from = null, DateTime? to = null);
    }
}
