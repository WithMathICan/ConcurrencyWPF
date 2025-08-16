using Microsoft.Extensions.DependencyInjection;
using PCTradeClient.Services;
using PCTradeClient.ViewModels;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;

namespace PCTradeClient {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // Register HttpClient (singleton is enough for most cases)
            services.AddSingleton(new HttpClient());

            // Register services
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            // Register ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<LoginViewModel>();

            Services = services.BuildServiceProvider();

            // Show MainWindow with injected DataContext
            var mainWindow = new MainWindow {
                DataContext = Services.GetRequiredService<MainViewModel>()
            };
            //mainWindow.Show();
        }
    }

}
