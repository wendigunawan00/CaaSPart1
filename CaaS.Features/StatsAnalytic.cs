using AutoMapper;
using CaaS.Domain;
using CaaS.DTO;
using CaaS.Logic;
using System.Linq;

namespace CaaS.Features
{
    public class StatsAnalytic: IAnalytic
    {
        private readonly IManagementLogic<Cart> logicCart;
        private readonly IManagementLogic<Order> logicOrder;
        private readonly IManagementLogic<Person> logicCustomer;
        private readonly IManagementLogic<Product> logicProduct;
        private readonly IManagementLogic<CartDetails> logicCartDetails;
        private readonly IManagementLogic<OrderDetails> logicOrderDetails;
        private IMapper _mapper;
        

        public StatsAnalytic(IManagementLogic<Cart> logicCart, IManagementLogic<Order> logicOrder,
            IManagementLogic<Person> logicCustomer, IManagementLogic<Product> logicProduct,
            IManagementLogic<CartDetails> logicCartDetails, IManagementLogic<OrderDetails> logicOrderDetails
            )
        {
            this.logicCart = logicCart ?? throw new ArgumentNullException(nameof(logicCart));
            this.logicOrder = logicOrder ?? throw new ArgumentNullException(nameof(logicOrder));
            this.logicCustomer = logicCustomer ?? throw new ArgumentNullException(nameof(logicCustomer));
            this.logicProduct = logicProduct ?? throw new ArgumentNullException(nameof(logicProduct));
            this.logicCartDetails = logicCartDetails ?? throw new ArgumentNullException(nameof(logicCartDetails));
            this.logicOrderDetails = logicOrderDetails ?? throw new ArgumentNullException(nameof(logicOrderDetails));         
            
        }
        public void setMapper(IMapper value)
        {
            _mapper = value;
        }
        
        public async Task<IEnumerable<BestSellerStatsDTO>> GetBestSeller(DateTime startDate, DateTime endDate, int n)
        {
            IEnumerable<OrderDetails> ordersDetails = await logicOrderDetails.Get();
            IEnumerable<Order> orders = (await logicOrder.Get()).Where(ods => ods.OrderDate >= startDate && ods.OrderDate <= endDate);
            var allSoldProducts = ordersDetails.Join(
                orders,
                od => od.OrderId, o => o.Id,
                (od, o) => new OrderDetailsStatsDTO(od.Id, od.OrderId, od.ProductId, od.UnitPrice, od.Quantity, od.Discount, o.CustId, o.CartId, o.OrderDate));
            // with constructor
            //var bestSeller = allSoldProducts.GroupBy(ods => ods.ProductId).Select(ods => new BestSellerStatsDTO( ods.Key, ods.Sum(o => o.Quantity) )).OrderBy(t1=>t1.TotalQuantity).TakeLast(3);
            // without constructor
            var bestSeller = allSoldProducts.GroupBy(ods => ods.ProductId).
                Select(ods => new BestSellerStatsDTO{ ProductId = ods.Key, TotalQuantity = ods.Sum(o => o.Quantity) }).
                OrderByDescending(t1=>t1.TotalQuantity).Take(n);
            return bestSeller;
            //return allSoldProducts;
        }
        
        public async Task<RevenueStatsDTO> GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            IEnumerable<OrderDetails> ordersDetails = await logicOrderDetails.Get();
            IEnumerable<Order> orders = (await logicOrder.Get()).Where(ods => ods.OrderDate >= startDate && ods.OrderDate <= endDate);
            var allSoldProducts = ordersDetails.Join(
                orders,
                od => od.OrderId, o => o.Id,
                (od, o) => new OrderDetailsStatsDTO(od.Id, od.OrderId, od.ProductId, od.UnitPrice, od.Quantity, od.Discount, o.CustId, o.CartId, o.OrderDate));

            // without constructor
            var totalQuantity = allSoldProducts.Sum(ods => ods.Quantity);
            var totalRevenue = allSoldProducts.Sum(ods => ods.UnitPrice * ods.Quantity - ods.Discount);
            return new RevenueStatsDTO { TotalRevenue=totalRevenue, TotalQuantity=totalQuantity};
        }
        
        public async Task<CartsStatsDTO> GetCartsStats(DateTime startDate)
        {
            IEnumerable<OrderDetails> ordersDetails = await logicOrderDetails.Get();
            IEnumerable<Order> orders = (await logicOrder.Get()).Where(ods => ods.OrderDate >= startDate && ods.OrderDate <= DateTime.Now);
            var allSoldProducts = ordersDetails.Join(
                orders,
                od => od.OrderId, o => o.Id,
                (od, o) => new OrderDetailsStatsDTO(od.Id, od.OrderId, od.ProductId, od.UnitPrice, od.Quantity, od.Discount, o.CustId, o.CartId, o.OrderDate));

            // without constructor
            return new CartsStatsDTO { TotalOpenCart = (await logicCart.Get()).Where(c =>c.Status.Equals("open")).Count(), TotalClosedCart = allSoldProducts.Count() };
        
        }
        
        public async Task<IEnumerable<OrderDTO>> GetAllOpenCarts()
        {
            var orders = await logicOrder.Get();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        
    }
}
