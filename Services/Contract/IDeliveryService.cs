namespace Formary.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Formary.Models;

    public interface IDeliveryService
    {
        Task<IList<Delivery>> QueryDeliveriesAsync(string query);
        Task<Delivery> GetDeliveryAsync(string id);
        Task<Delivery> AddDeliveryAsync(Delivery item);
        Task<Delivery> UpdateDeliveryAsync(string id, Delivery item);
        Task<Delivery> DeleteDeliveryAsync(string id);
    }
}
