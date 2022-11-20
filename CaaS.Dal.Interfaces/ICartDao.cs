using CaaS.Domain;

namespace CaaS.Dal.Interface
{
    public interface ICartDao
    {
        Task<IEnumerable<Cart>> FindAllAsync(string table);
        Task<Cart?> FindByIdAsync(string id, string table);

        Task<bool> UpdateAsync(Cart cart, string table);
        Task<bool> DeleteByIdAsync(string id, string table);
        Task<bool> StoreAsync(Cart cart, string table);
    }
}
