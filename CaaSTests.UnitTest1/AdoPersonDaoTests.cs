using CaaS.Dal.Ado;
using CaaS.Dal.Interface;
using CaaS.Domain;
using Dal.Common;
using Microsoft.Extensions.Configuration;
using MySqlX.XDevAPI.Relational;
using System;
using System.Configuration;

namespace CaaSTests.UnitTest1 
{ 
    [TestFixture]
    public class AdoPersonDaoTests    {
        
        private IPersonDao _personDao;
        private string _table = "Mandants";


        [SetUp]
        public void Setup()
        {
            IConfiguration configuration = ConfigurationUtil.GetConfiguration();
            IConnectionFactory? connectionFactory = DefaultConnectionFactory.FromConfiguration(configuration, "CaaSDbConnection");
            _personDao = new AdoPersonDao(connectionFactory);
        }

        [Test]
        public async Task TestFindAllAsync()
        {
            List<Person> personList = (await _personDao.FindAllAsync(_table)).ToList();

            Assert.True( personList.Count>1 && personList.Count <= 3);

        }

        [Test]
        public async Task TestFindByIdAsync()
        {
            Person? person1 = await _personDao.FindByIdAsync("mandant-1", _table);
            Assert.IsNotNull(person1);
            Assert.True(person1.FirstName == ("andrew"));

        }

        [Test] 
        public async Task TestUpdateAsync()
        {                        
            Person? person = await _personDao.FindByIdAsync("mandant-2", _table);
            person.FirstName = "ryoshi";
            await _personDao.UpdateAsync(person, _table);
            Assert.That(person.FirstName == "ryoshi");

        }

        [Test]
        public async Task TestDeleteByIdAsync()
        {
            Person? person = await _personDao.FindByIdAsync("mandant-2", _table);            
            await _personDao.DeleteByIdAsync(person.Id, _table);
            person = await _personDao.FindByIdAsync("mandant-2", _table);
            Assert.IsNull(person);

        }

        [Test]
        public async Task TestStoreAsync()
        {
            Person? person = await _personDao.FindByIdAsync("mandant-4", _table);
            Assert.IsNull(person);
            person= new Person("mandant-2", "ryo", "kimura", new DateTime(1971, 11, 7), "ryokimura@example.com", "addr-m2", "mandant");
            await _personDao.StoreAsync(person, _table);
            person = await _personDao.FindByIdAsync("mandant-2", _table);
            Assert.True(person.FirstName=="ryo");

            Person? person1 = new Person("mandant-3", "robert", "kimura", new DateTime(1974, 12, 7), "robertkimura@example.com", "addr-m3", "mandant");
            await _personDao.StoreAsync(person1, _table);
            Person? person2 = await _personDao.FindByIdAsync("mandant-3", _table);
            Assert.That(person2.FirstName, Is.EqualTo("robert"));

        }
    }
}