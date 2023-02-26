using CaaS.Dal.Ado;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using Dal.Common;
using Microsoft.Extensions.Configuration;


namespace CaaSTests.UnitTest1
{
    [TestFixture]
    public class AdoOrderDaoTests    {
        
        private IBaseDao<Order> _orderDao;
        private string _table = "Orders";


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

            Assert.IsTrue(990<=orderList.Count);

        }

        [TestCase("ord9-cust9-sh1", ExpectedResult = false)]
        [TestCase("ord9-cust9", ExpectedResult = true)]
        public async Task<bool> TestFindByIdAsync(string orderId)
        {
            Order? order1 = await _orderDao.FindByIdAsync(orderId, _table);
            bool isnull = (order1 is not null);
            return isnull;
        }

        [Test] 
        public async Task TestUpdateAsync()
        {                        
            Order? order = await _orderDao.FindByIdAsync("ord9-cust9", _table);
            order.OrderDate = new DateTime(2018,3,30);
            await _orderDao.UpdateAsync(order, _table);
            Order? order2 = await _orderDao.FindByIdAsync("ord9-cust9", _table);
            Assert.True(order2.OrderDate == new DateTime(2018, 3, 30));
            order.OrderDate = new DateTime(2015, 1, 9);
            await _orderDao.UpdateAsync(order, _table);
        }

       
    }
}