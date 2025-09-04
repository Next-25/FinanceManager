using FinanceManager.App.ViewModels;
using System;
using System.Windows;

namespace FinanceManager.App.Views
{
    public partial class EditTransactionDialog : Window
    {
        public EditTransactionDialog() => InitializeComponent();

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is EditTransactionViewModel vm)
            {
                DialogResult = true;
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBox tb)
            {
                tb.Dispatcher.BeginInvoke(new Action(() => tb.SelectAll()));
            }
        }
    }
}
