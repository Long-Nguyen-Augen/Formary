using Formary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formary.Services
{
    public interface ICustomerService
    {
        Task<IList<Customer>> QueryCustomersAsync(string query);
        Task<Customer> GetCustomerAsync(string id);
        Task<Customer> AddCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<Customer> DeleteCustomerAsync(string id);
    }
}
