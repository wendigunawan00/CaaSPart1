﻿using CaaS.Dal.Interfaces;
using MySqlX.XDevAPI.Relational;

namespace CaaS.Logic
{
    public class OrderManagementLogic<T> : IOrderManagementLogic<T> 
    {
        private readonly IBaseDao<T> baseDao;
        private string table;

        public OrderManagementLogic(IBaseDao<T> baseDao,string DBTableName)
        {
            this.baseDao = baseDao ?? throw new ArgumentNullException(nameof(baseDao));
            table = DBTableName;
        }       

        public async Task<IEnumerable<T>> Get()
        {
            return (IEnumerable<T>)await baseDao.FindAllAsync(table);           
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

    }
}