using FinanceManager.App.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FinanceManager.App.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MainViewModel vm && vm.EditCommand.CanExecute(null))
            {
                vm.EditCommand.Execute(null);
            }
        }
    }
}
