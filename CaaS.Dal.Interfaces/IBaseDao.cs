namespace CaaS.Dal.Interfaces;

public interface IBaseDao<T> : IDao
{
    Task<IEnumerable<T>> FindAllAsync(string table);
    Task<T?> FindByIdAsync(string id, string table);

    Task<bool> UpdateAsync(T obj, string table);
    Task<bool> DeleteByIdAsync(string id, string table);
    Task<bool> StoreAsync(T obj, string table);
}
