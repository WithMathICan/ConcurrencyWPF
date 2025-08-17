using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TraderClient.Views;

namespace TraderClient {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public static string? JwtToken { get; set; } // глобально для примера

        public MainWindow() {
            InitializeComponent();
            MainFrame.Navigate(new LoginPage(this)); // стартуем с логина
        }

        private void Login_Click(object sender, RoutedEventArgs e) {
            MainFrame.Navigate(new LoginPage(this));
        }

        private void Home_Click(object sender, RoutedEventArgs e) {
            if (JwtToken != null)
                MainFrame.Navigate(new HomePage(this));
            else
                MessageBox.Show("Please login first!");
        }

        private void Settings_Click(object sender, RoutedEventArgs e) {
            MainFrame.Navigate(new SettingsPage(this));
        }

        private void Logout_Click(object sender, RoutedEventArgs e) {
            JwtToken = null;
            MainFrame.Navigate(new LoginPage(this));
        }
    }
}