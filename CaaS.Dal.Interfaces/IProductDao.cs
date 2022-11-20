using CaaS.Domain;

namespace CaaS.Dal.Interface;

public interface IProductDao
{
    Task<IEnumerable<Product>> FindAllAsync(string table);
    Task<Product?> FindByIdAsync(string id, string table);

    Task<bool> UpdateAsync(Product product, string table);
    Task<bool> DeleteByIdAsync(string id, string table);
    Task<bool> StoreAsync(Product product, string table);
}
