using CaaS.Dal.Ado;
using CaaS.Dal.Interfaces;
using CaaS.Domain;
using Dal.Common;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace CaaSTests.UnitTests2
{
    public class AdoProductDaoLogicAPITests
    {
        private IBaseDao<Product> _ProductDao;
        private string _table = "Products";


        [SetUp]
        public void Setup()
        {
            _ProductDao = Substitute.For<IBaseDao<Product>>();
        }

        [Test]
        public void FindProductsByName_WhenFound_ReturnEnumerable()
        {
            IList<Product> products = new List<Product>(); 
            products.Add(new Product("arz-222", "Levetiracetam 500mg", 10, "10 Tablette", "Levetiracetam", "", "sh1"));
            _ProductDao.FindTByXAndY("Levetiracetam", "Levetiracetam", _table).Returns(products);
            Assert.That(_ProductDao.FindTByXAndY("Levetiracetam", "Levetiracetam", _table).Result, Is.EqualTo(expected: products));
        }
    }
}