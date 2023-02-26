using CaaS.Dal.Ado;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using Dal.Common;
using Microsoft.Extensions.Configuration;


namespace CaaSTests.UnitTest1
{

    [TestFixture]
    public class AdoAddressDaoTests
    {

        private IBaseDao<Address> _AddressDao;

        private string _table = "Addresses";


        [SetUp]
        public void Setup()
        {
            IConfiguration configuration = ConfigurationUtil.GetConfiguration();
            IConnectionFactory? connectionFactory = DefaultConnectionFactory.FromConfiguration(configuration, "CaaSDbConnection");
            _AddressDao = new AdoAddressDao(connectionFactory);
        }

        [Test]
        public async Task TestFindAllAsync()
        {
            List<Address> AddressList = (await _AddressDao.FindAllAsync(_table)).ToList();
            Assert.True(202 <= AddressList.Count);

        }

        [Test]
        public async Task TestFindByIdAsync()
        {
            Address? address1 = await _AddressDao.FindByIdAsync("addr-sh1", _table);
            Assert.IsNotNull(address1);
            Assert.True(address1.Province.Trim() == "Wien");

        }        

        [Test]
        public async Task TestStoreAsync()
        {
            Address? address = new Address("addr-sh12", "Gleinkernweg 6", "", 4420, "Wels", "Upper Austria", "Austria");
            await _AddressDao.StoreAsync(address, _table);
            Address? address2 = await _AddressDao.FindByIdAsync("addr-sh12", _table);
            Assert.True(address.Province == address2.Province);

        }              
        

        [Test]
        public async Task TestDeleteByIdAsync()
        {
            Address? address12 = new Address("addr-sh12", "Gleinkernweg 6", "", 4420, "Wels", "Upper Austria", "Austria");
            await _AddressDao.StoreAsync(address12, _table);
            Address? address = await _AddressDao.FindByIdAsync("addr-sh12", _table);
            Assert.IsNotNull(address);
            await _AddressDao.DeleteByIdAsync("addr-sh12", _table);
            address = await _AddressDao.FindByIdAsync("addr-sh12", _table);
            Assert.IsNull(address);

        }

        [Test]
        public async Task TestUpdateAsync()
        {           
            Address? address = new Address("addr-sh3", "Meilissenweg 3", "", 4020, "Linz", "Upper Austria", "Austria");
            await _AddressDao.StoreAsync(address, _table);
            Address? address2 = await _AddressDao.FindByIdAsync("addr-sh3", _table);
            Assert.That(address2.Province == "Upper Austria");
            address2.Province = "Lower Austria";
            Assert.That(address2.Province == "Lower Austria");
        }
    }
        
}