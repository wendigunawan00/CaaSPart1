using CaaS.Domain;

namespace CaaS.Dal.Interfaces;

public interface IBaseDao<T> : IGenericDao<T>
{
    Task<bool> UpdateAsync(T obj, string table);
    Task<bool> DeleteByIdAsync(string id, string table);
    Task<bool> StoreAsync(T obj, string table);

    Task<IEnumerable<T?>> FindTByXAndY(string criteriaX, string criteriaY, string table)
    {
        return (Task<IEnumerable<T?>>)Enumerable.Empty<T>();
    }

    
    
}
