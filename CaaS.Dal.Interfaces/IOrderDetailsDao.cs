using CaaS.Domain;

namespace CaaS.Dal.Interfaces;

public interface ICartDetailsDao 
{
    Task<IEnumerable<CartDetails>> FindAllAsync(string table);
    Task<CartDetails?> FindByIdAsync(string id, string table);

    Task<bool> UpdateAsync(CartDetails cartdetails, string table);
    Task<bool> DeleteByIdAsync(string id, string table);
    Task<bool> StoreAsync(CartDetails cartdetails, string table);
}
