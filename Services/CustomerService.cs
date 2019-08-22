using Formary.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formary.Services
{
    public class CustomerService : CosmosDbBaseService<Customer>, ICustomerService
    {
        public CustomerService(CosmosClient dbClient, Database database) : base(dbClient, database, "Customer", "/id")
        {
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            return await AddAsync(customer, customer.Id);
        }

        public async Task<Customer> DeleteCustomerAsync(string id)
        {
            return await DeleteAsync(id, id.ToString());
        }

        public async Task<Customer> GetCustomerAsync(string id)
        {
            return await GetAsync(id, id);
        }

        public async Task<IList<Customer>> QueryCustomersAsync(string query)
        {
            return await QueryAsync(query);
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            return await UpdateAsync(customer.Id, customer);
        }
    }
}
