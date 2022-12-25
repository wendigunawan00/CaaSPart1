using CaaS.Domain;

namespace CaaS.Dal.Interfaces;

public interface IOrderDetailsDao 
{
    Task<IEnumerable<OrderDetails>> FindAllAsync(string table);
    Task<OrderDetails?> FindByIdAsync(string id, string table);

    Task<bool> UpdateAsync(OrderDetails Orderdetails, string table);
    Task<bool> DeleteByIdAsync(string id, string table);
    Task<bool> StoreAsync(OrderDetails Orderdetails, string table);
}
