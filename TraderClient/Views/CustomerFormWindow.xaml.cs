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
using System.Windows.Shapes;
using TraderClient.Models;

namespace TraderClient.Views
{
    /// <summary>
    /// Interaction logic for CustomerFormWindow.xaml
    /// </summary>
    public partial class CustomerFormWindow : Window
    {
        public Customer Customer { get; private set; }
        public CustomerFormWindow()
        {
            InitializeComponent();
            Customer = new Customer();
            DataContext = Customer;
        }

        public CustomerFormWindow(Customer existing) {
            InitializeComponent();
            Customer = existing;
            DataContext = Customer;
        }

        private void Ok_Click(object sender, RoutedEventArgs e) {
            DialogResult = true; // Закроет окно и вернёт true
        }
    }
}
