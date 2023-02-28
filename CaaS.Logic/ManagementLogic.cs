using CaaS.Dal.Ado;
using CaaS.Dal.Interfaces;
using CaaS.Domain;

namespace CaaS.Logic
{
    public class ManagementLogic<T> : IManagementLogic<T>
    {
        private readonly IBaseDao<T> baseDao;
        private string table;


        public ManagementLogic(IBaseDao<T> baseDao,string DBTableName)
        {
            this.baseDao = baseDao ?? throw new ArgumentNullException(nameof(baseDao));            
            table = DBTableName;
        }       

        public async Task<IEnumerable<T>> Get()
        {
            return (IEnumerable<T>)await baseDao.FindAllAsync(table);           
        }

        public async Task<long> CountAll()
        {
            return await baseDao.CountAllElements(table);
        }
        public async Task<bool> Add(T obj)
        {
            return await baseDao.StoreAsync(obj, table);
        }

        public async Task<bool> Update(T obj)
        {
            return await baseDao.UpdateAsync(obj, table);
        }

        public async Task<bool> Delete(string id)
        {
            return await baseDao.DeleteByIdAsync(id, table);
        }

        public async Task<T?> Search(string id)
        {
            return await baseDao.FindByIdAsync(id, table);
        }
        
        public async Task<T?> GetLast()
        {
            return await baseDao.FindLastAsync(table);
        } 
                
        public async Task<IEnumerable<T>> GetTByShopId(string shopId)
        {
            return (IEnumerable<T>)await baseDao.FindTByX(shopId,table);
        } 
        
        public async Task<IEnumerable<T>> GetTByX(string criteriaX)
        {
            return (IEnumerable<T>)await baseDao.FindTByX(criteriaX,table);
        }

        public async Task<IEnumerable<T>> GetTByXAndY(string Name, string Description)
        {
           return (IEnumerable<T>) await baseDao.FindTByXAndY(Name, Description,table);
        }
    }
}