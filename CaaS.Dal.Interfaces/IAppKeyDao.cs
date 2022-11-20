using CaaS.Domain;

namespace CaaS.Dal.Interface
{
    public interface IAppKeyDao
    {
        Task<IEnumerable<AppKey>> FindAllAsync(string table);
        Task<AppKey?> FindByIdAsync(string id, string table);

        Task<bool> UpdateAsync(AppKey appkey, string table);
        Task<bool> DeleteByIdAsync(string id, string table);
        Task<bool> StoreAsync(AppKey appkey, string table);
    }
}
