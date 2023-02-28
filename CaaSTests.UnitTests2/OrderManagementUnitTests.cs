using CaaS.DTO;
using CaaS.Logic;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;

namespace CaaSTests.UnitTests2
{
    public class OrderManagementUnitTest
    {
        public static IEnumerable<CartDetailsDTO> CartDetailsTestCases
        {
            get
            {
                yield return new CartDetailsDTO("cartDet1-cart10-cust2000", "cart10-cust2000", "prod200", 20, "sh5");
                yield return new CartDetailsDTO("cartDet2-cart2-cust2001", "cart10-cust2001", "prod201", 28, "sh5");
                yield return new CartDetailsDTO("cartDet3-cart5-cust2002", "cart10-cust2002", "prod202", 29, "sh5");
            }
        }
        public static IEnumerable<OrderDetailsDTO> OrderDetailsTestCases
        { 
            get
            {
                yield return new OrderDetailsDTO("ordDet1-cart10-cust2000", "ord10-cust2000", "prod200", 20,5,0 ,"sh5");
                yield return new OrderDetailsDTO("ordDet2-cart2-cust2001", "ord10-cust2001", "prod201", 28,5,0, "sh5");
                yield return new OrderDetailsDTO("ordDet3-cart5-cust2002", "ord10-cust2002", "prod202", 29,5,0, "sh5");
            }
        }
        public static IEnumerable<CartDTO> CartsTestCases
        {
            get
            {
                yield return new CartDTO("cart10-cust2000", "cust2000", "open");
                yield return new CartDTO("cart2-cust2001", "cust2001", "open");
                yield return new CartDTO("cart5-cust2002", "cust2002", "open");
            }
        } 
        
        public static IEnumerable<double> DiscountsTestCases
        {
            get
            {
                yield return 15.0;
                yield return 29.0;
                yield return 30.0;
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
        
        public static IEnumerable<IDiscountRule> DiscountRulesTestCases
        {
            get
            {                             
                yield return (new MinimumOrderRules { MinimumOrderQuantity = 10 });
                yield return (new PeriodeRules {startDate = new DateTime(2022, 1, 3), endDate = new DateTime(2022, 1, 5) });
                yield return (new MinimumOrderRules { MinimumOrderQuantity = 50 });
             
            }           
        } 
        public static IEnumerable<IDiscountAction> DiscountActionsTestCases
        {
            get
            {                          
                yield return( new FixDiscount { FixRate = 1 });
                yield return( new PercentageDiscount { PercentageRate = 2.5 });
                yield return( new FixDiscount { FixRate = 5 });
            }
        }

        public static string OrderId { get; private set; }

        private IOrderManagementLogic orderMgtLogic;

        [SetUp]
        public void Setup()
        {
            this.orderMgtLogic = Substitute.For<IOrderManagementLogic>();            
        }



        [TestCase("cart1-custX","prod123" ,ExpectedResult = false)]
        [TestCase("t29","notprod1" ,ExpectedResult = true)]
        [TestCase("z-", "notprod2",ExpectedResult = true)]
        public bool ShowOpenCartDetailsByCartIdAndProductID_WhenThereisOpenCart_ReturnFound( string openCartId, string productId)
        {
            CartDetailsDTO? returned = new CartDetailsDTO("cartDet1-cart1-custX","cart1-custX","custX",5,"open");
            orderMgtLogic.ShowOpenCartDetailsByCartIdAndProductId("cart1-custX","prod123").Returns(returned);
            bool result= orderMgtLogic.ShowOpenCartDetailsByCartIdAndProductId(openCartId,productId).Result is null;
            return result;
        }
        
        [TestCase("cart1-custX" ,ExpectedResult = false)]
        [TestCase("t29" ,ExpectedResult = true)]
        [TestCase("z-",ExpectedResult = true)]
        public bool ShowOpenCartDetailsByCartId_WhenThereisOpenCart_ReturnEnumerable( string openCartId)
        {
            List<CartDetailsDTO> cartDetailsList = new List<CartDetailsDTO>{ 
                new CartDetailsDTO("cartDet1-cart1-custX", "cart1-custX", "custX", 5, "open") };          
            orderMgtLogic.ShowOpenCartDetailsByCartId("cart1-custX").Returns(cartDetailsList);
            bool result= orderMgtLogic.ShowOpenCartDetailsByCartId(openCartId).Result.IsNullOrEmpty();
            return result;
        } 

        [Test]
        public void ShowOpenCartByCustomerID_WhenThereisOpenCart_ReturnFound()
        {
            List<CartDTO> returned = new List<CartDTO>();
            returned.Add(new CartDTO("cart1-custX","custX","open"));
            orderMgtLogic.ShowOpenCartByCustomerID("custX").Returns(returned);
            Assert.That(orderMgtLogic.ShowOpenCartByCustomerID("custX").Result, Is.EqualTo(expected: returned));
            orderMgtLogic.Received().ShowOpenCartByCustomerID("custX");
        }

        [Test]
        public void ShowOpenCartByCustomerID_WhenNoOpenCart_ReturnNotFound()
        {           
            orderMgtLogic.ShowOpenCartByCustomerID("custY").Returns(Enumerable.Empty<CartDTO>());
            Assert.That(orderMgtLogic.ShowOpenCartByCustomerID("custY").Result, Is.EqualTo(expected: Enumerable.Empty<CartDTO>()));
            orderMgtLogic.Received().ShowOpenCartByCustomerID("custY");
        }



        [TestCase("arz-7", ExpectedResult =  false)]
        [TestCase("t29", ExpectedResult = true)]
        [TestCase("z-", ExpectedResult = true)]
        public bool IsValidProduct_WhenInvalidOrValid_ReturnProductDTO(string productId)
        {            
            orderMgtLogic.isValidProduct("arz-7").Returns((ProductDTO?)new ProductDTO("arz-7","Depakine",0,"","","",""));
            var result= orderMgtLogic.isValidProduct(productId).Result is null;            
            
            return result;
        }  
        
        [TestCase("cust2000", ExpectedResult =  false)]
        [TestCase("ggt29", ExpectedResult = true)]
        [TestCase("zz-", ExpectedResult = true)]
        public bool IsValidCustomer_WhenInvalidOrValid_ReturnPersonDTO(string customerId)
        {            
            orderMgtLogic.isValidCustomer("cust2000").Returns((PersonDTO?)new PersonDTO("cust2000", "Pate", "", new DateTime(2000,2,22), "", "", ""));
            var result= orderMgtLogic.isValidCustomer(customerId).Result is null;            
            
            return result;
        }

        [Test]
        public void ShowOpenCart_WhenFound_ReturnEnumerable()
        {
            List<CartDTO> cartDTOList = new List<CartDTO>();
            cartDTOList.Add(new CartDTO("cart1-cust2000", "cust2000", "open"));
            orderMgtLogic.ShowOpenCart().Returns(cartDTOList);
            var result= orderMgtLogic.ShowOpenCart().Result.IsNullOrEmpty();            
            Assert.That(result, Is.False);
        } 
        
        [Test]
        public void CreateCart_WhenCreated_ReturnIt()
        {
            CartDTO cartDTO = new CartDTO("cart10-cust2000", "cust2000", "open");
            orderMgtLogic.CreateCart("Cust2000").Returns(cartDTO);
            var result= orderMgtLogic.CreateCart("Cust2000").Result.Id;            
            Assert.That(result, Is.EqualTo("cart10-cust2000"));
        }
        
        [Test]
        public void CreateCartDetails_WhenCreated_ReturnIt()
        {
            CartDetailsDTO cartDetailsDTO = new CartDetailsDTO("cartDet1-cart10-cust2000", "cart10-cust2000","prod200",20, "sh5");
            orderMgtLogic.CreateCartDetails("cart10-cust2000", "prod200", 20).Returns(cartDetailsDTO);
            var result= orderMgtLogic.CreateCartDetails("cart10-cust2000", "prod200", 20).Result.ProductId;            
            Assert.That(result, Is.EqualTo("prod200"));
        }


        [TestCase("cart10-cust2000", ExpectedResult = true)]
        [TestCase("ggt29", ExpectedResult = false)]
        [TestCase("zz-", ExpectedResult = false)]
        public bool DeleteCartDetailsByCartId_WhenDeletedOrNot_ReturnBoolean(string cartId)
        {
            orderMgtLogic.DeleteCartDetailsByCartId("cart10-cust2000").Returns(true);
            var result= orderMgtLogic.DeleteCartDetailsByCartId(cartId).Result;
            return result;
        } 
        
        [TestCaseSource(nameof(CartDetailsTestCases))]       
        public void UpdateCartDetails_WhenUpdated_ReturnTrue(CartDetailsDTO cartDet) 
        { 
            orderMgtLogic.UpdateCartDetails(cartDet).Returns(true);
            var check= orderMgtLogic.UpdateCartDetails(cartDet);            

            Assert.True(check.Result==true);       
        }

        [TestCaseSource(nameof(CartDetailsTestCases))]       
        public void UpdateCartDetails_WhenUpdatedOrNot_ReturnFalse(CartDetailsDTO cartDet) 
        { 
            orderMgtLogic.UpdateCartDetails(cartDet).Returns(false);
            var check= orderMgtLogic.UpdateCartDetails(cartDet);
            Assert.IsTrue(check.Result==false);       
        }


        [TestCaseSource(nameof(RunningIndex))]
        public void CreateOrder_WhenSuccess_ReturnOrderDetailsDTO(int runningIndex)
        {
            
            OrderDetailsDTO[] odDto = OrderDetailsTestCases.ToArray();
            
            orderMgtLogic.CreateOrder(
                CartsTestCases.ToArray()[runningIndex],
                CartDetailsTestCases.ToArray()[runningIndex],
                DiscountsTestCases.ToArray()[runningIndex],
                DiscountRulesTestCases.ToList(), 
                DiscountActionsTestCases.ToList())               
                .Returns(odDto[runningIndex]);
            string[] prodList = { "prod200", "prod201", "prod202" };
            orderMgtLogic.CreateOrder(
               CartsTestCases.ToArray()[runningIndex],
               CartDetailsTestCases.ToArray()[runningIndex],
               DiscountsTestCases.ToArray()[runningIndex],              
               DiscountRulesTestCases.ToList(), DiscountActionsTestCases.ToList()
               );
            Assert.That(prodList[runningIndex], Is.EqualTo(odDto[runningIndex].ProductId));
        }
    }
}