using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TraderClient.Services;

namespace TraderClient.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly MainWindow _mainWindow;
        private readonly AuthenticationService _authService = new();

        public LoginPage(MainWindow mainWindow) {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Login_Click(object sender, RoutedEventArgs e) {
            var token = _authService.Login(UsernameBox.Text, PasswordBox.Password);
            if (token != null) {
                MainWindow.JwtToken = token;
                MessageBox.Show("Login successful!");
                _mainWindow.MainFrame.Navigate(new HomePage(_mainWindow));
            } else {
                MessageBox.Show("Invalid username or password");
            }
        }
    }
}
