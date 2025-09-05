using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.App.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Today;
        [Required]
        public TransactionType Type { get; set; } = TransactionType.Расход;
        [Required]
        public TransactionCategory Category { get; set; } = TransactionCategory.Другое;
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
