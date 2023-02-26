using CaaS.Dal.Ado;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using Dal.Common;
using Microsoft.Extensions.Configuration;


namespace CaaSTests.UnitTest1
{
    [TestFixture]
    public class AdoCartDaoTests    {
        
        private IBaseDao<Cart> _CartDao;
        private string _table = "Carts";


        [SetUp]
        public void Setup()
        {
            IConfiguration configuration = ConfigurationUtil.GetConfiguration();
            IConnectionFactory? connectionFactory = DefaultConnectionFactory.FromConfiguration(configuration, "CaaSDbConnection");
            _CartDao = new AdoCartDao(connectionFactory);
        }

        [Test]
        public async Task TestFindAllAsync()
        {
            List<Cart> CartList = (await _CartDao.FindAllAsync(_table)).ToList();
            Assert.True(999 <= CartList.Count);
        }

        [Test]
        public async Task TestFindByIdAsync()
        {
            Cart? cart1 = await _CartDao.FindByIdAsync("cart10-cust1", _table);
            Assert.IsNotNull(cart1);
            Assert.True(cart1.Status== ("open")&&cart1.CustId=="cust1");

        }

        [Test] 
        public async Task TestUpdateAsync()
        {                        
            Cart? cart = await _CartDao.FindByIdAsync("cart10-cust1", _table);
            Assert.That(cart.Status == "open");
            cart.Status = "closed";
            await _CartDao.UpdateAsync(cart, _table);
            Cart? cart2 = await _CartDao.FindByIdAsync("cart10-cust1", _table);
            Assert.That(cart2.Status == "closed");
            cart2.Status = "open";
            await _CartDao.UpdateAsync(cart2, _table);
            Cart? cart3 = await _CartDao.FindByIdAsync("cart10-cust1", _table);
            Assert.That(cart3.Status == "open");
        }
               
        [Test]
        public async Task TestStoreAsync()
        {
            await _CartDao.DeleteByIdAsync("cart10-cust6", _table);
            Cart? cart = await _CartDao.FindByIdAsync("cart10-cust6", _table);
            Assert.IsNull(cart);
            cart= new Cart("cart10-cust6", "cust6", "open");
            await _CartDao.StoreAsync(cart, _table);
            Cart? cart2 = await _CartDao.FindByIdAsync("cart10-cust6", _table);
            Assert.True(cart2.CustId=="cust6");
        }

        [Test]
        public async Task TestDeleteByIdAsync()
        {
            await _CartDao.DeleteByIdAsync("cart10-cust6", _table);
            Cart? cart2 = await _CartDao.FindByIdAsync("cart10-cust6", _table);
            Assert.IsNull(cart2);

        }

    }
}