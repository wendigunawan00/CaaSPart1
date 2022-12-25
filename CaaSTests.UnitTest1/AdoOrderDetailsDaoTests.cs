using CaaS.Dal.Ado;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using Dal.Common;
using Microsoft.Extensions.Configuration;


namespace CaaSTests.UnitTest1
{
    [TestFixture]
    public class AdoOrderDetailsDaoTests    {
        
        private IBaseDao<OrderDetails> _orderDetailsDao;
        private string _table = "OrdersDetailsShop1";


        [SetUp]
        public void Setup()
        {
            IConfiguration configuration = ConfigurationUtil.GetConfiguration();
            IConnectionFactory? connectionFactory = DefaultConnectionFactory.FromConfiguration(configuration, "CaaSDbConnection");
            _orderDetailsDao = new AdoOrderDetailsDao(connectionFactory);
        }

        [Test]
        public async Task TestFindAllAsync()
        {
            List<OrderDetails> orderdetailsList = (await _orderDetailsDao.FindAllAsync(_table)).ToList();

            Assert.IsTrue(2933== orderdetailsList.Count);

        }

        [Test]
        public async Task TestFindByIdAsync()
        {
            OrderDetails? orderdetails1 = await _orderDetailsDao.FindByIdAsync("od-10", _table);
            Assert.IsNotNull(orderdetails1);
            Assert.True(orderdetails1.ProductId == ("arz-16"));

        }

        [Test] 
        public async Task TestUpdateAsync()
        {                        
            OrderDetails? orderdetails = await _orderDetailsDao.FindByIdAsync("od-10", _table);
            orderdetails.UnitPrice = 6.75;
            await _orderDetailsDao.UpdateAsync(orderdetails, _table);
            OrderDetails? orderdetails2 = await _orderDetailsDao.FindByIdAsync("od-10", _table);
            Assert.True(orderdetails2.UnitPrice== 6.75);
            orderdetails2.UnitPrice = 6.75;
            await _orderDetailsDao.UpdateAsync(orderdetails2, _table);


        }

       
    }
}