using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderClient.Models;

namespace TraderClient.Services
{
    public class DataService {
        private readonly List<Customer> _customers = new() {
            new Customer { Id = 1, Name = "Alice", City = "London" },
            new Customer { Id = 2, Name = "Bob", City = "Paris" }
        };

        public List<Customer> GetCustomers() => new(_customers);

        public void AddCustomer(Customer c) => _customers.Add(c);

        public void DeleteCustomer(Customer c) => _customers.Remove(c);

        public void UpdateCustomer(Customer c) {
            var existing = _customers.Find(x => x.Id == c.Id);
            if (existing != null) {
                existing.Name = c.Name;
                existing.City = c.City;
            }
        }
    }
}
