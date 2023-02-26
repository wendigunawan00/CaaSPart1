using CaaS.Dal.Ado;
using CaaS.Dal.Interfaces;
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
        
        private IGenericDao<Person> _personDao;
        private IBaseDao<Person> _personBaseDao;
        private string _table = "Mandants";


        [SetUp]
        public void Setup()
        {
            IConfiguration configuration = ConfigurationUtil.GetConfiguration();
            IConnectionFactory? connectionFactory = DefaultConnectionFactory.FromConfiguration(configuration, "CaaSDbConnection");
            _personDao = new AdoPersonDao(connectionFactory);
            _personBaseDao = new AdoPersonDao(connectionFactory);
        }

        [Test]
        public async Task TestFindAllAsync()
        {
           var personList = (await _personDao.FindAllAsync(_table));

            Assert.True( personList.Count()>=1 );

        }

        [Test]
        public async Task TestFindByIdAsync()
        {
            Person? person1 = await _personDao.FindByIdAsync("mnd1", _table);
            Assert.IsNotNull(person1);
            Assert.True(person1.FirstName == ("andrew"));

        }

        [Test] 
        public async Task TestUpdateAsync()
        {                        
            Person? person = await _personDao.FindByIdAsync("mnd2", _table);
            person.FirstName = "ryoshi";
            await _personBaseDao.UpdateAsync(person, _table);
            Assert.That(person.FirstName == "ryoshi");

        }

        [Test]
        public async Task TestDeleteByIdAsync()
        {
            Person? person = await _personDao.FindByIdAsync("mnd2", _table);            
            await _personBaseDao.DeleteByIdAsync(person.Id, _table);
            person = await _personDao.FindByIdAsync("mnd2", _table);
            Assert.IsNull(person);
            person = await _personDao.FindByIdAsync("mnd3", _table);            
            await _personBaseDao.DeleteByIdAsync(person.Id, _table);
            person = await _personDao.FindByIdAsync("mnd3", _table);
            Assert.IsNull(person);
        }

        [Test]
        public async Task TestStoreAsync()
        {
            Person? person = await _personDao.FindByIdAsync("mnd4", _table);
            Assert.IsNull(person);
            person= new Person("mnd2", "ryo", "kimura", new DateTime(1971, 11, 7), "ryokimura@example.com", "addr-mnd2", "sh2","SuperUser!2");
            await _personBaseDao.StoreAsync(person, _table);
            person = await _personDao.FindByIdAsync("mnd2", _table);
            Assert.True(person.FirstName=="ryo");

            Person? person1 = new Person("mnd3", "robert", "kimura", new DateTime(1974, 12, 7), "robertkimura@example.com", "addr-mnd3", "sh3","SuperUser!3");
            await _personBaseDao.StoreAsync(person1, _table);
            Person? person2 = await _personDao.FindByIdAsync("mnd3", _table);
            Assert.That(person2.FirstName, Is.EqualTo("robert"));

        }
    }
}