using CaaS.Domain;

namespace CaaS.Dal.Interface
{
    public interface IOrderDao
    {
        Task<IEnumerable<Order>> FindAllAsync(string table);
        Task<Order?> FindByIdAsync(string id, string table);

        Task<bool> UpdateAsync(Order order, string table);
        Task<bool> DeleteByIdAsync(string id, string table);
        Task<bool> StoreAsync(Order order, string table);
    }
}
