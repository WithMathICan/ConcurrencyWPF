using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderClient.Models;

namespace TraderClient.Services
{
    public class DataService {
        private readonly List<Customer> _customers = [
            new Customer { Id = 1, Name = "Alice", City = "London" },
            new Customer { Id = 2, Name = "Bob", City = "Paris" }
        ];

        public List<Customer> GetCustomers() => [.. _customers];

        public void AddCustomer(Customer c) {
            _customers.Add(c);
        }

        public void DeleteCustomer(Customer c) {
            var customerToRemove = _customers.FirstOrDefault(x => x.Id == c.Id);
            if (customerToRemove != null)_customers.Remove(customerToRemove);
        }

        public void UpdateCustomer(Customer c) {
            var existing = _customers.Find(x => x.Id == c.Id);
            if (existing != null) {
                existing.Name = c.Name;
                existing.City = c.City;
            }
        }
    }
}
