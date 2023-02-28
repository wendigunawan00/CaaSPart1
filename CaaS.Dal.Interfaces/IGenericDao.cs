using CaaS.Domain;

namespace CaaS.Dal.Interfaces;

public interface IGenericDao<T> : IDao
{
    Task<IEnumerable<T>> FindAllAsync(string table);
    Task<T?> FindByIdAsync(string id, string table);
    Task<T?> FindLastAsync(string table) { return default!; }
    Task<long> CountAllElements(string table);
    Task<IEnumerable<T?>> FindTByX(string criteriaX, string table)
    {
        return (Task<IEnumerable<T?>>)Enumerable.Empty<T>();
    }
}
