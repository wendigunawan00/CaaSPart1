using CaaS.Domain;

namespace CaaS.Dal.Interface
{
    public interface IAddressDao
    {
        Task<IEnumerable<Address>> FindAllAsync(string table);
        Task<Address?> FindByIdAsync(string id, string table);
        Task<bool> UpdateAsync(Address address, string table);
        Task<bool> DeleteByIdAsync(string id, string table);
        Task<bool> StoreAsync(Address address, string table);
    }
}
