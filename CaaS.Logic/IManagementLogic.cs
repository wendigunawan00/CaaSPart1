using CaaS.Domain;
using CaaS.DTO;

namespace CaaS.Logic;

public interface IManagementLogic<T>
{
    Task<IEnumerable<T>> Get();
    Task<T?> Search(string id);
    Task<T?> GetLast();
    Task<long> CountAll();
    Task<bool> Add(T obj);
    Task<bool> Update(T obj);
    Task<bool> Delete(string id);
    Task<IEnumerable<T>> GetTByShopId(string shopName);   
    Task<IEnumerable<T>> GetTByXAndY(string Name, string Description);   
    Task<IEnumerable<T>> GetTByX(string criteriaX);   
}
