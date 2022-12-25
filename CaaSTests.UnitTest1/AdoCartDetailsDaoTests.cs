using CaaS.Dal.Ado;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using Dal.Common;
using Microsoft.Extensions.Configuration;

namespace CaaSTests.UnitTest1
{
    [TestFixture]
    public class AdoCartDetailsTests    {
        
        private IBaseDao<CartDetails> _cartdetailsDao;
        private string _table = "CartsDetailsShop1";


        [SetUp]
        public void Setup()
        {
            IConfiguration configuration = ConfigurationUtil.GetConfiguration();
            IConnectionFactory? connectionFactory = DefaultConnectionFactory.FromConfiguration(configuration, "CaaSDbConnection");
            _cartdetailsDao = new AdoCartDetailsDao(connectionFactory);
        }

        [Test]
        public async Task TestFindAllAsync()
        {
            List<CartDetails> cartdetailsList = (await _cartdetailsDao.FindAllAsync(_table)).ToList();
            Console.WriteLine(cartdetailsList.Count);
            Assert.True(cartdetailsList.Count>0);

        }

        [Test]
        public async Task TestFindByIdAsync()
        {
            CartDetails? cartdetails1 = await _cartdetailsDao.FindByIdAsync("cd-9", _table);
            Assert.IsNotNull(cartdetails1);
            Assert.True(cartdetails1.ProductId == ("arz-18"));

        }

        [Test] 
        public async Task TestUpdateAsync()
        {                        
            CartDetails? cartdetails = await _cartdetailsDao.FindByIdAsync("cd-8", _table);
            cartdetails.Quantity = 0;
            await _cartdetailsDao.UpdateAsync(cartdetails, _table);
            Assert.That(cartdetails.Quantity == 0);

            cartdetails.Quantity = 18;
            await _cartdetailsDao.UpdateAsync(cartdetails, _table);
            Assert.That(cartdetails.Quantity == 18);

        }

        [Test]
        public async Task TestDeleteByIdAsync()
        {
            CartDetails? cartdetails = await _cartdetailsDao.FindByIdAsync("cd-4", _table);            
            Assert.IsNotNull(cartdetails);
            await _cartdetailsDao.DeleteByIdAsync("cd-4", _table);
            cartdetails = await _cartdetailsDao.FindByIdAsync("cd-4", _table);
            Assert.IsNull(cartdetails);

        }

        [Test]
        public async Task TestStoreAsync()
        {
            CartDetails? cartdetails = await _cartdetailsDao.FindByIdAsync("cd-4", _table);
            Assert.IsNull(cartdetails);
            cartdetails= new CartDetails("cd-4", "cart-6-c-4", "arz-94", 7);
            await _cartdetailsDao.StoreAsync(cartdetails, _table);
           

            CartDetails? cartdetails1 = new CartDetails("cd-1100", "cart-10-c-1", "arz-23", 20);
            await _cartdetailsDao.StoreAsync(cartdetails1, _table);
            CartDetails? cartdetails2 = await _cartdetailsDao.FindByIdAsync("cd-1100", _table);
            Assert.That(cartdetails2.CartId, Is.EqualTo((cartdetails1.CartId)));

        }
    }
}