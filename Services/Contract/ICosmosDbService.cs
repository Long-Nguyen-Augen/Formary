using System.Collections.Generic;
using System.Threading.Tasks;

namespace Formary.Services
{
    public interface ICosmosDbService<T> where T : class
    {
        Task<T> AddAsync(T item, string partitionKeyValue);
        Task<T> DeleteAsync(string id, string partitionKeyValue);
        Task<T> GetAsync(string id, string partitionKeyValue);
        Task<IList<T>> QueryAsync(string queryString);
        Task<T> UpdateAsync(string partitionKeyValue, T item);
    }
}
