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
        private string _table = "CartsDetails";


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

        [TestCase("cartDet9-sh1", ExpectedResult = false)]
        [TestCase("cartDet1-cart1-cust1-sh2", ExpectedResult = true)]        
        public async Task<bool> TestFindByIdAsync(string cartDetId)
        {
            CartDetails? cartdetails1 = await _cartdetailsDao.FindByIdAsync(cartDetId, _table);
            bool isnull= (cartdetails1 is not null);
            return isnull;

        }

        [Test] 
        public async Task TestUpdateAsync()
        {                        
            CartDetails? cartdetails = await _cartdetailsDao.FindByIdAsync("cartDet1-cart2-cust1-sh1", _table);
            cartdetails.Quantity = 0;
            await _cartdetailsDao.UpdateAsync(cartdetails, _table);
            Assert.That(cartdetails.Quantity == 0);

            cartdetails.Quantity = 36;
            await _cartdetailsDao.UpdateAsync(cartdetails, _table);
            Assert.That(cartdetails.Quantity == 36);

        }

        [Test]
        public async Task TestDeleteByIdAsync()
        {
            CartDetails? cartdetails = await _cartdetailsDao.FindByIdAsync("cartDet4-sh1", _table);            
            Assert.IsNotNull(cartdetails);
            await _cartdetailsDao.DeleteByIdAsync("cartDet4-sh1", _table);
            cartdetails = await _cartdetailsDao.FindByIdAsync("cartDet4-sh1", _table);
            Assert.IsNull(cartdetails);

        }

        [Test]
        public async Task TestStoreAsync()
        {
            CartDetails? cartdetails = await _cartdetailsDao.FindByIdAsync("cartDet4-sh1", _table);
            Assert.IsNull(cartdetails);
            cartdetails= new CartDetails("cartDet4-sh1", "cart6-cust4-sh1", "arz-94", 7,"sh1");
            await _cartdetailsDao.StoreAsync(cartdetails, _table);
           

            CartDetails? cartdetails1 = new CartDetails("cartDet1100-sh1", "cart10-cust1-sh1", "arz-23", 20,"sh1");
            await _cartdetailsDao.StoreAsync(cartdetails1, _table);
            CartDetails? cartdetails2 = await _cartdetailsDao.FindByIdAsync("cartDet1100-sh1", _table);
            Assert.That(cartdetails2.CartId, Is.EqualTo((cartdetails1.CartId)));

        }
    }
}