namespace Formary.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Formary.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;

    public class DeliveryService : CosmosDbBaseService<Delivery>, IDeliveryService
    {
        public DeliveryService(CosmosClient dbClient, Database database): base(dbClient, database, "Delivery", "/id")
        {   
        }
        
        public async Task<Delivery> AddDeliveryAsync(Delivery item)
        {
            return await AddAsync(item, item.Id);            
        }

        public async Task<Delivery> DeleteDeliveryAsync(string id)
        {
            return await DeleteAsync(id, id);            
        }

        public async Task<Delivery> GetDeliveryAsync(string id)
        {
            return await GetAsync(id, id);            
        }

        public async Task<IList<Delivery>> QueryDeliveriesAsync(string queryString)
        {
            return await QueryAsync(queryString);            
        }

        public async Task<Delivery> UpdateDeliveryAsync(string id, Delivery item)
        {
            return await UpdateAsync(id, item);            
        }
    }
}
