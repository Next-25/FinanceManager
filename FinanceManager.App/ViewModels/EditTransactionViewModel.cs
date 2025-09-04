using FinanceManager.App.Models;
using System;

namespace FinanceManager.App.ViewModels
{
    public class EditTransactionViewModel : BaseViewModel
    {
        public Transaction Item { get; }

        public EditTransactionViewModel(Transaction item)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }

        public DateTime Date
        {
            get => Item.Date;
            set
            {
                if (Item.Date != value)
                {
                    Item.Date = value;
                    Raise(nameof(Date));
                }
            }
        }

        public TransactionType Type
        {
            get => Item.Type;
            set
            {
                if (Item.Type != value)
                {
                    Item.Type = value;
                    Raise(nameof(Type));
                }
            }
        }

        public TransactionCategory Category
        {
            get => Item.Category;
            set
            {
                if (Item.Category != value)
                {
                    Item.Category = value;
                    Raise(nameof(Category));
                }
            }
        }

        public decimal Amount
        {
            get => Item.Amount;
            set
            {
                if (Item.Amount != value)
                {
                    Item.Amount = value;
                    Raise(nameof(Amount));
                }
            }
        }

        public string? Description
        {
            get => Item.Description;
            set
            {
                if (Item.Description != value)
                {
                    Item.Description = value;
                    Raise(nameof(Description));
                }
            }
        }

        public Array Types => Enum.GetValues(typeof(TransactionType));
        public Array Categories => Enum.GetValues(typeof(TransactionCategory));
    }
}
