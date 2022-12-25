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
        private string _table = "CartsShop1";


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
            Assert.True(999 == CartList.Count);
        }

        [Test]
        public async Task TestFindByIdAsync()
        {
            Cart? cart1 = await _CartDao.FindByIdAsync("cart-3-c-7", _table);
            Assert.IsNotNull(cart1);
            Assert.True(cart1.Status== ("open")&&cart1.CustId=="cust-7");

        }

        [Test] 
        public async Task TestUpdateAsync()
        {                        
            Cart? cart = await _CartDao.FindByIdAsync("cart-10-c-17", _table);
            Assert.That(cart.Status == "open");
            cart.Status = "closed";
            await _CartDao.UpdateAsync(cart, _table);
            Assert.That(cart.Status == "closed");
            cart.Status = "open";
            await _CartDao.UpdateAsync(cart, _table);
            Assert.That(cart.Status == "open");

        }

        [Test]
        public async Task TestDeleteByIdAsync()
        {
            await _CartDao.DeleteByIdAsync("cart-4-c-6", _table);
            Cart? cart2 = await _CartDao.FindByIdAsync("cart-4-c-6", _table);
            Assert.IsNull(cart2);

        }

        [Test]
        public async Task TestStoreAsync()
        {
            Cart? cart = await _CartDao.FindByIdAsync("cart-4-c-6", _table);
            Assert.IsNull(cart);
            cart= new Cart("cart-4-c-6", "cust-6", "open");
            await _CartDao.StoreAsync(cart, _table);
            Cart? cart2 = await _CartDao.FindByIdAsync("cart-4-c-6", _table);
            Assert.True(cart2.CustId=="cust-6");


        }
    }
}