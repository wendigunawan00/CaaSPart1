using CaaS.Dal.Ado;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using Dal.Common;
using Microsoft.Extensions.Configuration;


namespace CaaSTests.UnitTest1
{
    public class AdoAppKeyDaoTests
    {
        private IBaseDao<AppKey> _appKeyDao;
        private readonly string _table = "AppKeys";


        [SetUp]
        public void Setup()
        {
            IConfiguration configuration = ConfigurationUtil.GetConfiguration();
            IConnectionFactory? connectionFactory = DefaultConnectionFactory.FromConfiguration(configuration, "CaaSDbConnection");
            _appKeyDao = new AdoAppKeyDao(connectionFactory);
        }

        [Test]
        public async Task TestFindAllAsync()
        {
            List<AppKey> appKeyList = (await _appKeyDao.FindAllAsync(_table)).ToList();
            Assert.True( appKeyList.Count <=2&& appKeyList.Count >0);
        }

        [Test]
        public async Task TestFindByIdAsync()
        {
            AppKey? appKey1 = await _appKeyDao.FindByIdAsync("ak-s1", _table);
            Assert.IsNotNull(appKey1);

        }

        [Test]
        public async Task TestUpdateAsync()
        {
            AppKey? appKey = await _appKeyDao.FindByIdAsync("ak-s2", _table);
            Assert.That(appKey.AppKeyName == "jungle");
            appKey= new AppKey("ak-s2", "jingle","np");
            await _appKeyDao.UpdateAsync(appKey, _table);
            AppKey? appKey2 = await _appKeyDao.FindByIdAsync("ak-s2", _table);
            Assert.That(appKey2.AppKeyName == "jingle");
            appKey2.AppKeyName = "jungle";
            await _appKeyDao.UpdateAsync(appKey2, _table);

        }

        [Test]
        public async Task TestDeleteByIdAsync()
        {
            await _appKeyDao.DeleteByIdAsync("ak-s2", _table);
            AppKey? appKey2 = await _appKeyDao.FindByIdAsync("ak-s2", _table);
            Assert.IsNull(appKey2);

        }

        [Test]
        public async Task TestStoreAsync()
        {
            AppKey? appKey = await _appKeyDao.FindByIdAsync("ak-s2", _table);
            Assert.IsNull(appKey);
            appKey = new AppKey("ak-s2", "jungle","np");
            await _appKeyDao.StoreAsync(appKey, _table);
            AppKey? appKey2 = await _appKeyDao.FindByIdAsync("ak-s2", _table);
            Assert.True(appKey2.AppKeyName == "jungle");

        }
    }
}
