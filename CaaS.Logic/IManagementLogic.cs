using CaaS.Domain;

namespace CaaS.Logic;

public interface IManagementLogic<T>
{
    Task<IEnumerable<T>> Get();
    Task<T?> Search(string id);
    Task<bool> Add(T obj);
    Task<bool> Update(T obj);
    Task<bool> Delete(string id);  

}
