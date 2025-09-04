using FinanceManager.App.Data;
using FinanceManager.App.Views;
using System.Windows;

namespace FinanceManager.App
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DbInitializer.EnsureCreated();

            var main = new MainWindow();
            MainWindow = main;
            main.Show();
        }
    }
}
