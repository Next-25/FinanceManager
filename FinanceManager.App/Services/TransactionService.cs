using FinanceManager.App.Data;
using FinanceManager.App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManager.App.Services
{
    public class TransactionService : ITransactionService
    {
        public async Task<List<Transaction>> GetAllAsync(DateTime? from = null, DateTime? to = null)
        {
            await using var db = new AppDbContext();
            var q = db.Transactions.AsQueryable();
            if (from.HasValue) q = q.Where(x => x.Date >= from.Value.Date);
            if (to.HasValue) q = q.Where(x => x.Date <= to.Value.Date);
            return await q.OrderByDescending(x => x.Date).ThenByDescending(x => x.Id).ToListAsync();
        }

        public async Task<Transaction> AddAsync(Transaction t)
        {
            await using var db = new AppDbContext();
            db.Transactions.Add(t);
            await db.SaveChangesAsync();
            return t;
        }

        public async Task UpdateAsync(Transaction t)
        {
            await using var db = new AppDbContext();
            db.Attach(t);
            db.Entry(t).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Transaction t)
        {
            await using var db = new AppDbContext();
            db.Attach(t);
            db.Transactions.Remove(t);
            await db.SaveChangesAsync();
        }

        public async Task<decimal> GetIncomeSumAsync(DateTime? from = null, DateTime? to = null)
        {
            await using var db = new AppDbContext();
            var q = db.Transactions.Where(x => x.Type == TransactionType.Income);
            if (from.HasValue) q = q.Where(x => x.Date >= from.Value.Date);
            if (to.HasValue) q = q.Where(x => x.Date <= to.Value.Date);
            return await q.SumAsync(x => (decimal?)x.Amount) ?? 0m;
        }

        public async Task<decimal> GetExpenseSumAsync(DateTime? from = null, DateTime? to = null)
        {
            await using var db = new AppDbContext();
            var q = db.Transactions.Where(x => x.Type == TransactionType.Expense);
            if (from.HasValue) q = q.Where(x => x.Date >= from.Value.Date);
            if (to.HasValue) q = q.Where(x => x.Date <= to.Value.Date);
            return await q.SumAsync(x => (decimal?)x.Amount) ?? 0m;
        }
    }
}
