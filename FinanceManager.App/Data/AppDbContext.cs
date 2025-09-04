using FinanceManager.App.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace FinanceManager.App.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var folder = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
                "FinanceManager");
            Directory.CreateDirectory(folder);
            var dbPath = Path.Combine(folder, "finances.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
