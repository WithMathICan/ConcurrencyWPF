using System.Windows;
using System.Windows.Controls;
using TraderClient.Models;
using TraderClient.Services;

namespace TraderClient.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page {
        private readonly MainWindow _mainWindow;
        private readonly DataService _dataService = new();

        public HomePage(MainWindow mainWindow) {
            InitializeComponent();
            _mainWindow = mainWindow;
            LoadData();
        }

        private void LoadData() {
            CustomersGrid.ItemsSource = _dataService.GetCustomers();
        }

        private void Add_Click(object sender, RoutedEventArgs e) {
            var newCustomer = new Customer { Id = 99, Name = "New User", City = "Berlin" };
            _dataService.AddCustomer(newCustomer);
            LoadData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e) {
            if (CustomersGrid.SelectedItem is Customer c) {
                c.Name = "Edited Name";
                _dataService.UpdateCustomer(c);
                LoadData();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e) {
            if (CustomersGrid.SelectedItem is Customer c) {
                _dataService.DeleteCustomer(c);
                LoadData();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e) {
            LoadData();
        }
    }
}
