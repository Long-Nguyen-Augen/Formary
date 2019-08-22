using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formary.Services
{
    public abstract class CosmosDbBaseService<T> : ICosmosDbService<T> where T : class
    {
        protected Container _container;
        protected Database _database;
        protected CosmosClient _dbClient;
        protected string _containerName = string.Empty;
        protected string _partitionKey = "/id";

        public CosmosDbBaseService(
            CosmosClient dbClient,
            Database database,
            string containerName,
            string partitionKey)
        {
            _dbClient = dbClient;
            _database = database;
            _containerName = containerName;
            _partitionKey = partitionKey;

            _container = _database.CreateContainerIfNotExistsAsync(_containerName, _partitionKey).GetAwaiter().GetResult();
        }

        public virtual async Task<T> AddAsync(T item, string partitionKeyValue)
        {
            var result = await _container.CreateItemAsync<T>(item, new PartitionKey(partitionKeyValue));
            return result.Resource;
        }

        public virtual async Task<T> DeleteAsync(string id, string partitionKeyValue)
        {
            var result = await _container.DeleteItemAsync<T>(id, new PartitionKey(partitionKeyValue));
            return result.Resource;
        }

        public virtual async Task<T> GetAsync(string id, string partitionKeyValue)
        {
            try
            {
                ItemResponse<T> response = await this._container.ReadItemAsync<T>(id, new PartitionKey(partitionKeyValue));
                return response.Resource;
            }
            catch (CosmosException)
            {                
                return default(T);
            }
        }

        public virtual async Task<IList<T>> QueryAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public virtual async Task<T> UpdateAsync(string partitionKeyValue, T item)
        {
            var result = await _container.UpsertItemAsync<T>(item, new PartitionKey(partitionKeyValue));
            return result.Resource;
        }
    }
}
