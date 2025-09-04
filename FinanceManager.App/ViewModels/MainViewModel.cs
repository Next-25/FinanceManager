using FinanceManager.App.Models;
using FinanceManager.App.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace FinanceManager.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ITransactionService _service;

        public ObservableCollection<Transaction> Transactions { get; } = new ObservableCollection<Transaction>();

        private Transaction? _selected;
        public Transaction? Selected { get => _selected; set { Set(ref _selected, value); EditCommand.RaiseCanExecuteChanged(); DeleteCommand.RaiseCanExecuteChanged(); } }

        private DateTime? _from = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        public DateTime? From { get => _from; set { Set(ref _from, value); _ = ReloadAsync(); } }

        private DateTime? _to = DateTime.Today;
        public DateTime? To { get => _to; set { Set(ref _to, value); _ = ReloadAsync(); } }

        private decimal _incomeSum;
        public decimal IncomeSum { get => _incomeSum; set { Set(ref _incomeSum, value); Raise(nameof(Balance)); } }

        private decimal _expenseSum;
        public decimal ExpenseSum { get => _expenseSum; set { Set(ref _expenseSum, value); Raise(nameof(Balance)); } }

        public decimal Balance => IncomeSum - ExpenseSum;

        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand ClearFilterCommand { get; }

        public MainViewModel() : this(new TransactionService()) { }

        public MainViewModel(ITransactionService service)
        {
            _service = service;
            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit, () => Selected != null);
            DeleteCommand = new RelayCommand(Delete, () => Selected != null);
            ClearFilterCommand = new RelayCommand(() => { From = null; To = null; });

            _ = ReloadAsync();
        }

        public async Task ReloadAsync()
        {
            var items = await _service.GetAllAsync(From, To);
            // Обновляем коллекцию в UI-потоке
            Application.Current.Dispatcher.Invoke(() =>
            {
                Transactions.Clear();
                foreach (var t in items) Transactions.Add(t);
            });

            IncomeSum = await _service.GetIncomeSumAsync(From, To);
            ExpenseSum = await _service.GetExpenseSumAsync(From, To);

            EditCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
        }

        private void Add()
        {
            var vm = new EditTransactionViewModel(new Transaction { Type = TransactionType.Expense, Date = DateTime.Today });
            if (DialogService.ShowEditDialog(vm) == true)
            {
                Task.Run(async () =>
                {
                    await _service.AddAsync(vm.Item);
                    await ReloadAsync();
                });
            }
        }

        private void Edit()
        {
            if (Selected == null) return;

            var copy = new Transaction
            {
                Id = Selected.Id,
                Date = Selected.Date,
                Type = Selected.Type,
                Category = Selected.Category,
                Amount = Selected.Amount,
                Description = Selected.Description
            };

            var vm = new EditTransactionViewModel(copy);
            if (DialogService.ShowEditDialog(vm) == true)
            {
                Task.Run(async () =>
                {
                    await _service.UpdateAsync(vm.Item);
                    await ReloadAsync();
                });
            }
        }

        private void Delete()
        {
            if (Selected == null) return;
            if (MessageBox.Show("Удалить выбранную операцию?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Task.Run(async () =>
                {
                    await _service.DeleteAsync(Selected);
                    await ReloadAsync();
                });
            }
        }
    }

    public static class DialogService
    {
        public static bool? ShowEditDialog(EditTransactionViewModel vm)
        {
            var dlg = new Views.EditTransactionDialog { DataContext = vm, Owner = Application.Current.MainWindow };
            return dlg.ShowDialog();
        }
    }
}
