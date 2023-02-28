using CaaS.Dal.Ado;
using CaaS.Domain;
using CaaS.DTO;
using CaaS.Logic;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using NSubstitute.Extensions;
using NSubstitute.ReceivedExtensions;

namespace CaaSTests.UnitTests2
{
    public class ManagementUnitTest
    {

        public static IEnumerable<Product> ProductTestCases
        {
            get
            {
                yield return new Product("prod110","Knorr Curry", 10, "12 Stück", "Gewürze","ny", "sh5");
                yield return new Product("prod120","ERoller", 390, "ABS 7.6Ah","Transport","ny","sh5");
                yield return new Product("prod139","Viva Ram 16GB", 39, "For all comps","Computer Zubehör","ny","sh5");
            }
        }

        public static IEnumerable<int> RunningIndex
        {
            get
            {
                yield return 0;
                yield return 1;
                yield return 2;
            }
        }
        public static string OrderId { get; private set; }

        private IManagementLogic<Product> mgtLogic;

        [SetUp]
        public void Setup()
        {
            this.mgtLogic = Substitute.For<IManagementLogic<Product>>();
        }
        [Test]
        public void GetT_WhenFound_ReturnIEnumerableT()
        {
            List<Product> prodList=new List<Product>();
            prodList.Add(new Product("prod5","Metformin",2,"ny","ny","ny","ny"));
            this.mgtLogic.Get().Returns(prodList);
            this.mgtLogic.Get();
            this.mgtLogic.Received().Get();
            Assert.IsInstanceOf<IManagementLogic<Product>>(this.mgtLogic);
            Assert.That(prodList[0].Name, Is.EqualTo("Metformin"));
        }
        
        [TestCase("prod5", ExpectedResult = true)]
        [TestCase("prod89", ExpectedResult = false)]
        public bool SearchT_WhenFound_ReturnT(string id)
        { 
            this.mgtLogic.Search("prod5").Returns(new Product("prod5", "Metformin", 2, "ny", "ny", "ny", "ny"));
            var result = this.mgtLogic.Search(id).Result is not null;
            this.mgtLogic.Received().Search(id);
            return result;
        }

        [Test]
        public void GetLastT_WhenFound_ReturnT()
        { 
            this.mgtLogic.GetLast().Returns(new Product("prod5", "Metformin", 2, "ny", "ny", "ny", "ny"));
            var result = this.mgtLogic.GetLast().Result is not null;
            this.mgtLogic.Received().GetLast();
            Assert.That(result==true);
        }  
        
        [Test]
        public void GetLastT_WhenNotFound_ReturnNull()
        { 
            this.mgtLogic.GetLast().Returns((Product?)null);
            var result = this.mgtLogic.GetLast().Result is not null;
            this.mgtLogic.Received().GetLast();
            Assert.That(result==false);
        }
        
        [Test]
        public void CountAllT_WhenFound_ReturnInt()
        { 
            this.mgtLogic.CountAll().Returns(15);
            var result = this.mgtLogic.CountAll().Result > 14;
            this.mgtLogic.Received().CountAll();
            Assert.That(result==true);
        }  
        
        [Test]
        public void CountAllT_WhenNotFound_ReturnInt()
        { 
            this.mgtLogic.CountAll().Returns(0);
            var result = this.mgtLogic.CountAll().Result == 0;
            this.mgtLogic.Received().CountAll();
            Assert.That(result==true);
        }

        [TestCaseSource(nameof(ProductTestCases))]
        public void AddT_WhenAddedOrNot_ReturnTrueOrFalse(Product prod)
        {
            Product wrongProd = new Product("notCorrectProd", "nrX", 2, "1", "test", "test", "shtest");
            this.mgtLogic.Add(prod).Returns(true);
            var checkTrue= this.mgtLogic.Add(prod);
            this.mgtLogic.Received().Add(prod); 
            var checkFalse= this.mgtLogic.Add(wrongProd);
            this.mgtLogic.Received().Add(wrongProd);
            Assert.IsTrue(checkTrue.Result == true);
            Assert.IsTrue(checkFalse.Result == false);
        }
        
        [TestCaseSource(nameof(ProductTestCases))]
        public void UpdateT_WhenUpdatedOrNot_ReturnTrueOrFalse(Product prod)
        {
            Product wrongProd = new Product("notCorrectProd", "nrX", 2, "1", "test", "test", "shtest");
            this.mgtLogic.Update(prod).Returns(true);
            var checkTrue= this.mgtLogic.Update(prod);
            this.mgtLogic.Received().Update(prod); 
            var checkFalse= this.mgtLogic.Update(wrongProd);
            this.mgtLogic.Received().Update(wrongProd);
            Assert.IsTrue(checkTrue.Result == true);
            Assert.IsTrue(checkFalse.Result == false);
        } 
        
        [TestCaseSource(nameof(ProductTestCases))]
        public void DeleteT_WhenDeletedOrNot_ReturnTrueOrFalse(Product prod)
        {
            Product wrongProd = new Product("notCorrectProd", "nrX", 2, "1", "test", "test", "shtest");
            this.mgtLogic.Delete(prod.Id).Returns(true);
            var checkTrue= this.mgtLogic.Delete(prod.Id);
            this.mgtLogic.Received().Delete(prod.Id); 
            var checkFalse= this.mgtLogic.Delete(wrongProd.Id);
            this.mgtLogic.Received().Delete(wrongProd.Id);
            Assert.IsTrue(checkTrue.Result == true);
            Assert.IsTrue(checkFalse.Result == false);
        } 
        
        [TestCaseSource(nameof(RunningIndex))]
        public void GetTByShopId_WhenDeletedOrNot_ReturnTrueOrFalse(int index)
        {
            List<Product> prodList = ProductTestCases.ToList();
            string[] shopId = { "sh1", "sh3", "sh5" };
            bool[] foundProduct = { true, true, false };
            this.mgtLogic.GetTByShopId(shopId[2]).Returns(prodList);
            var check= this.mgtLogic.GetTByShopId(shopId[index]).Result.IsNullOrEmpty();
            this.mgtLogic.Received().GetTByShopId(shopId[index]);
            Assert.That(check == foundProduct[index]);
        }               
        
        [TestCaseSource(nameof(RunningIndex))]
        public void GetTByXAndY_WhenDeletedOrNot_ReturnTrueOrFalse(int index)
        {
            List<Product> prodList = ProductTestCases.ToList();
            string[] prodName = { "Knorr Eroller", "Bike","Wok pan" };
            string[] prodDesc = { "Computer", "Travel" ,"Audio"};           
            bool[] foundProduct = { false, true,true };
            this.mgtLogic.GetTByXAndY(prodName[0], prodDesc[0]).Returns(prodList);
            var check= this.mgtLogic.GetTByXAndY(prodName[index], prodDesc[index]).Result.IsNullOrEmpty();
            this.mgtLogic.Received().GetTByXAndY(prodName[index], prodDesc[index]);
            Assert.That(check == foundProduct[index]);
        }
    }
}