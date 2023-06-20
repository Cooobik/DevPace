using DevPace.Desktop.Services;
using DevPace.Desktop.ViewModels;
using DevPace.Desktop.Views;
using System.Configuration;
using System.Net.Http;
using System.Windows;

namespace DevPace.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            var httpClient = new HttpClient();

            var customerService = new CustomerService(httpClient, apiUrl);

            var mainViewModel = new MainViewModel(customerService);
            var mainWindow = new MainWindow();
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();
        }
    }
}
