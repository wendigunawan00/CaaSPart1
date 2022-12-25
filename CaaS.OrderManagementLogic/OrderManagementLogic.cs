using CaaS.Dal.Interfaces;
using MySqlX.XDevAPI.Relational;

namespace CaaS.Logic
{
    public class OrderManagementLogic<T>: IOrderManagementLogic<T>
    {
        private readonly IBaseDao<T> pDao;
        private string table;

        public OrderManagementLogic(IBaseDao<T> personDao,string DBTableName)
        {
            this.pDao = personDao ?? throw new ArgumentNullException(nameof(personDao));
            table = DBTableName;
        }

        public async Task<IEnumerable<T>> GetCustomers()
        {
            return (IEnumerable<T>)await pDao.FindAllAsync(table);           
        }

       
    }
}