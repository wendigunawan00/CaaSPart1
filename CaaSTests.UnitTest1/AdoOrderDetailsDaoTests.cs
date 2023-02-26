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
        private string _table = "OrdersDetails";


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
            Assert.IsTrue(2000< orderdetailsList.Count);
        }

        [Test]
        public async Task TestFindByIdAsync()
        {
            OrderDetails? orderdetails1 = await _orderDetailsDao.FindByIdAsync("ordDet2-ord2-cust1-sh1", _table);
            Assert.IsNotNull(orderdetails1);
            Assert.True(orderdetails1.ProductId == ("arz-16"));

        }

        [Test] 
        public async Task TestUpdateAsync()
        {                        
            OrderDetails? orderdetails = await _orderDetailsDao.FindByIdAsync("ordDet1-ord1-cust1-sh2", _table);
            orderdetails.UnitPrice = 6.75;
            await _orderDetailsDao.UpdateAsync(orderdetails, _table);
            OrderDetails? orderdetails2 = await _orderDetailsDao.FindByIdAsync("ordDet1-ord1-cust1-sh2", _table);
            Assert.True(orderdetails2.UnitPrice== 6.75);
            orderdetails2.UnitPrice =52;
            await _orderDetailsDao.UpdateAsync(orderdetails2, _table);
        }

       
    }
}