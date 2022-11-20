using CaaS.Dal.Ado;
using CaaS.Dal.Interface;
using CaaS.Domain;
using Dal.Common;
using Microsoft.Extensions.Configuration;


namespace CaaSTests.UnitTest1
{

    [TestFixture]
    public class AdoAddressDaoTests
    {

        private IAddressDao _AddressDao;

        private string _table = "AddressesShops";


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
            Assert.True(4 >= AddressList.Count);

        }

        [Test]
        public async Task TestFindByIdAsync()
        {
            Address? address1 = await _AddressDao.FindByIdAsync("addr-sh1", _table);
            Assert.IsNotNull(address1);
            Assert.True(address1.Province.Trim() == "Wien");

        }

        [Test]
        public async Task TestUpdateAsync()
        {
            Address? address = await _AddressDao.FindByIdAsync("addr-sh3", _table);
            address.Province = "Lower Austria";
            await _AddressDao.UpdateAsync(address, _table);
            Assert.That(address.Province == "Lower Austria");
            Address? address2 = new Address("addr-sh3", "Meilissenweg 3", "", 4020, "Linz", "Upper Austria", "Austria");
            await _AddressDao.UpdateAsync(address2, _table);
            Assert.That(address2.Province == "Upper Austria");
        }


        [Test]
        public async Task TestDeleteByIdAsync()
        {
            Address? address = await _AddressDao.FindByIdAsync("addr-sh12", _table);
            Assert.IsNotNull(address);
            await _AddressDao.DeleteByIdAsync("addr-sh12", _table);
            address = await _AddressDao.FindByIdAsync("addr-sh12", _table);
            Assert.IsNull(address);

        }

        [Test]
        public async Task TestStoreAsync()
        {
            Address? address = new Address("addr-sh12", "Gleinkernweg 6", "", 4420, "Wels", "Upper Austria", "Austria");
            await _AddressDao.StoreAsync(address, _table);
            Address? address2 = await _AddressDao.FindByIdAsync("addr-sh12", _table);
            Assert.True(address.Province == address2.Province);

        }
    }
        
}