using CaaS.Dal.Ado;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using Dal.Common;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace CaaSTests.UnitTests2
{
    public class AdoPersonDaoLogicAPITests
    {
        private IBaseDao<Person> _PersonDao;
        private string _table = "Customers";


        [SetUp]
        public void Setup()
        {
            _PersonDao = Substitute.For<IBaseDao<Person>>();
        }

        [Test]
        public void FindPersonByEmail_WhenFound_ReturnPerson()
        {
            IList<Person> PersonX = new List<Person>();
                PersonX.Add(new Person("cust1000","max","mustermann",new DateTime(2000,1,31),"maxmustermann@example.com","addr1000","active","blackx")); 
            _PersonDao.FindTByX("maxmustermann@example.com",_table).Returns(PersonX);
            Assert.That(_PersonDao.FindTByX("maxmustermann@example.com", _table).Result, Is.EqualTo(expected: PersonX));
        }
    }
}