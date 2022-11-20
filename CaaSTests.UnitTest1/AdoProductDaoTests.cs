using CaaS.Dal.Ado;
using CaaS.Dal.Interface;
using CaaS.Domain;
using Dal.Common;
using Microsoft.Extensions.Configuration;


namespace CaaSTests.UnitTest1
{
    [TestFixture]
    public class AdoProductDaoTests    {
        
        private IProductDao _ProductDao;
        private string _table = "ProductShop2";


        [SetUp]
        public void Setup()
        {
            IConfiguration configuration = ConfigurationUtil.GetConfiguration();
            IConnectionFactory? connectionFactory = DefaultConnectionFactory.FromConfiguration(configuration, "CaaSDbConnection");
            _ProductDao = new AdoProductDao(connectionFactory);
        }

        [Test]
        public async Task TestFindAllAsync()
        {
            List<Product> ProductList = (await _ProductDao.FindAllAsync(_table)).ToList();

            Assert.IsTrue(109==ProductList.Count);

        }

        [Test]
        public async Task TestFindByIdAsync()
        {
            Product? Product1 = await _ProductDao.FindByIdAsync("978-0-7503-1047-5", _table);
            Assert.IsNotNull(Product1);
            Assert.True(Product1.Name == ("Introduction to Networks of Networks"));

        }

        [Test] 
        public async Task TestUpdateAsync()
        {                        
            Product? product = await _ProductDao.FindByIdAsync("978-0-7503-1645-3", _table);
            product.AmountDesc = "100 pc";
            await _ProductDao.UpdateAsync(product, _table);
            Product? product2 = await _ProductDao.FindByIdAsync("978-0-7503-1645-3", _table);
            Assert.That(product2.AmountDesc == product.AmountDesc);

        }

        [Test]
        public async Task TestDeleteByIdAsync()
        {
            Product? product = await _ProductDao.FindByIdAsync("978-0-7503-1645-3", _table);
            await _ProductDao.DeleteByIdAsync(product.Id, _table);
            product = await _ProductDao.FindByIdAsync("978-0-7503-1645-3", _table);
            Assert.IsNull(product);

        }

        [Test]
        public async Task TestStoreAsync()
        {
            Product? product = await _ProductDao.FindByIdAsync("mandant-4", _table);
            Assert.IsNull(product);
            product= new Product("978-0-7503-1645-3", "Functional Carbon Materials", 52.3,"1 pc",  "not yet", "not yet");
            await _ProductDao.StoreAsync(product, _table);
            Product? product2 = await _ProductDao.FindByIdAsync("978-0-7503-1645-3", _table);
            Assert.True(product.Name== product2.Name);

            Product? product3 = new Product("978-0-7503-1047-5", "Introduction to Networks of Networks",44.1, "1pc", "not yet", "not yet");
            await _ProductDao.StoreAsync(product3, _table);
            Product? product4 = await _ProductDao.FindByIdAsync("978-0-7503-1047-5", _table);
            Assert.That(product4.Id ,Is.EqualTo(product4.Id));

        }
    }
}