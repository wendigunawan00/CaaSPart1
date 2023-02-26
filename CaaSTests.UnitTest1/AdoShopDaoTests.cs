using CaaS.Dal.Ado;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using Dal.Common;
using Microsoft.Extensions.Configuration;
using MySqlX.XDevAPI.Relational;
using System;
using System.Configuration;

namespace CaaSTests.UnitTest1
{
    [TestFixture]
    public class AdoShopDaoTests    {
        
        private IBaseDao<Shop> _shopDao;        
        private string _table = "Shops";


        [SetUp]
        public void Setup()
        {
            IConfiguration configuration = ConfigurationUtil.GetConfiguration();
            IConnectionFactory? connectionFactory = DefaultConnectionFactory.FromConfiguration(configuration, "CaaSDbConnection");
            _shopDao = new AdoShopDao(connectionFactory);
        }

        [Test]
        public async Task TestFindAllAsync()
        {
            List<Shop> ShopList = (await _shopDao.FindAllAsync(_table)).ToList();
            Assert.True( ShopList.Count<=3 && ShopList.Count>0);
        }

        [Test]
        public async Task TestFindByIdAsync()
        {
            Shop? shop1 = await _shopDao.FindByIdAsync("sh1", _table);
            Assert.IsNotNull(shop1);

        }

        [Test] 
        public async Task TestUpdateAsync()
        {                        
            Shop? shop = await _shopDao.FindByIdAsync("sh1", _table);
            Assert.That(shop.Name== "LoveRead");
            shop.Name = "MedStorDrug";
            await _shopDao.UpdateAsync(shop, _table);
            shop = await _shopDao.FindByIdAsync("sh1", _table);
            Assert.That(shop.Name== "MedStorDrug");
            shop.Name = "LoveRead";
            await _shopDao.UpdateAsync(shop, _table);
            shop= await _shopDao.FindByIdAsync("sh1", _table);
            Assert.That(shop.Name == "LoveRead");

        }

        [Test]
        public async Task TestDeleteByIdAsync()
        {
            Shop? shop = await _shopDao.FindByIdAsync("sh5", _table);
            Assert.IsNull(shop);
            await _shopDao.DeleteByIdAsync("sh5", _table);
            Shop? shop2 = await _shopDao.FindByIdAsync("sh5", _table);
            Assert.IsNull(shop2);
        }

        
    }
}