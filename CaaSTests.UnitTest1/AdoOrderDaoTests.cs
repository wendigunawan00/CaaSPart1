using CaaS.Dal.Ado;
using CaaS.Dal.Interface;
using CaaS.Domain;
using Dal.Common;
using Microsoft.Extensions.Configuration;


namespace CaaSTests.UnitTest1
{
    [TestFixture]
    public class AdoOrderDaoTests    {
        
        private IOrderDao _orderDao;
        private string _table = "OrdersShop1";


        [SetUp]
        public void Setup()
        {
            IConfiguration configuration = ConfigurationUtil.GetConfiguration();
            IConnectionFactory? connectionFactory = DefaultConnectionFactory.FromConfiguration(configuration, "CaaSDbConnection");
            _orderDao = new AdoOrderDao(connectionFactory);
        }

        [Test]
        public async Task TestFindAllAsync()
        {
            List<Order> orderList = (await _orderDao.FindAllAsync(_table)).ToList();

            Assert.IsTrue(990==orderList.Count);

        }

        [Test]
        public async Task TestFindByIdAsync()
        {
            Order? order1 = await _orderDao.FindByIdAsync("or-9-c-9", _table);
            Assert.IsNotNull(order1);
            Assert.True(order1.CustId == ("cust-9"));

        }

        [Test] 
        public async Task TestUpdateAsync()
        {                        
            Order? order = await _orderDao.FindByIdAsync("or-9-c-9", _table);
            order.OrderDate = new DateTime(2018,3,30);
            await _orderDao.UpdateAsync(order, _table);
            Order? order2 = await _orderDao.FindByIdAsync("or-9-c-9", _table);
            Assert.That(order2.OrderDate== new DateTime(2018, 3, 30));

        }

       
    }
}