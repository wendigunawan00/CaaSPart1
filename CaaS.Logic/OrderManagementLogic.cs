using AutoMapper;
using CaaS.DTO;
using CaaS.Domain;

namespace CaaS.Logic
{
    public class OrderManagementLogic :IOrderManagementLogic
    {
        private readonly IManagementLogic<Cart> logicCart;
        private readonly IManagementLogic<Order> logicOrder;
        private readonly IManagementLogic<Product> logicProduct;
        private readonly IManagementLogic<CartDetails> logicCartDetails;
        private readonly IManagementLogic<OrderDetails> logicOrderDetails;
        private IMapper _mapper;



        public OrderManagementLogic(IManagementLogic<Cart> logicCart, IManagementLogic<Order> logicOrder,
            IManagementLogic<Product> logicProduct,
            IManagementLogic<CartDetails> logicCartDetails, IManagementLogic<OrderDetails> logicOrderDetails           
            )
        {
            this.logicCart = logicCart ?? throw new ArgumentNullException(nameof(logicCart));
            this.logicOrder = logicOrder ?? throw new ArgumentNullException(nameof(logicOrder));
            this.logicProduct = logicProduct ?? throw new ArgumentNullException(nameof(logicProduct));
            this.logicCartDetails = logicCartDetails ?? throw new ArgumentNullException(nameof(logicCartDetails));
            this.logicOrderDetails = logicOrderDetails ?? throw new ArgumentNullException(nameof(logicOrderDetails));
        }
                

        public void setMapper(IMapper value)
        {
            _mapper = value;
        }

        public async Task<IEnumerable<CartDTO>> ShowOpenCart()
        {
            var carts = await logicCart.Get();
            return _mapper.Map <IEnumerable <CartDTO>>( carts.Where(c=>c.Status.Equals("open")) )?? Enumerable.Empty<CartDTO>();
        }

        public async Task<IEnumerable<CartDTO>> ShowOpenCartByCustomerID(string customerId)
        {
            var carts = await logicCart.Get();            
            return _mapper.Map<IEnumerable<CartDTO>>(carts.Where(p => p.Status.Equals("open") && p.CustId.Equals(customerId)));
        }
        
        public async Task<IEnumerable<CartDetailsDTO?>> ShowOpenCartDetailsByProductId(string openCartId, string productId)
        {
            var allCartDetails = await logicCartDetails.Get();
            var cartdetailsToBeShown= allCartDetails.Where(p => p.ProductId.Equals(productId) && p.CartId.Equals(openCartId)).FirstOrDefault();
            return _mapper.Map<IEnumerable<CartDetailsDTO?>>(cartdetailsToBeShown);
        } 
        
        public async Task<IEnumerable<CartDetailsDTO?>> ShowOpenCartDetailsByCartId(string openCartId)
        {
            var allCartDetails = await logicCartDetails.Get();
            var cartdetailsToBeShown= allCartDetails.Where(p=>p.CartId.Equals(openCartId));
            return _mapper.Map<IEnumerable<CartDetailsDTO>>(cartdetailsToBeShown);
        }

        public async Task<CartDTO> CreateCart(string customerId)
        {
            var carts = await logicCart.Get();
            var lastCartId = carts.Where(c => c.CustId.Equals(customerId)).Count() + 1;
            var newCart = new Cart($"cart-{lastCartId}-{customerId.Replace("cust", "c")}",
                customerId, "open"
                );
            await logicCart.Add(newCart);
            return _mapper.Map<CartDTO>(newCart);
            
        }

        public async Task<CartDetailsDTO> CreateCartDetails(string openCartId, string productId, double quantity)
        {
            var allCartDetails= await logicCartDetails.Get();
            var newCartDetails = new CartDetails(
                $"cd-{allCartDetails.Count() + 1}",
                openCartId,
                productId,
                quantity
                );
            await logicCartDetails.Add(newCartDetails);
            return _mapper.Map<CartDetailsDTO>(newCartDetails);
        }
               
        public async Task<CartDetailsDTO> UpdateCartDetails(CartDetailsDTO cartDetailsToBeUpdated)
        {
            await logicCartDetails.Update(_mapper.Map<CartDetails>(cartDetailsToBeUpdated));
            return _mapper.Map<CartDetailsDTO >( cartDetailsToBeUpdated);
        }
                

        public async Task<OrderDetailsDTO> CreateOrder(CartDTO openCart, CartDetailsDTO? openCartDetails, double discount)
        {   
            openCart.Status = "closed";
            var newOrderId = openCart.Id.Replace("cart", "or");
            await logicCart.Update(_mapper.Map<Cart>(openCart));
            var newOrder = new Order(newOrderId, openCart.CustId, openCart.Id, DateTime.Now);
            await logicOrder.Add(newOrder);
            var product = await logicProduct.Search(openCartDetails.ProductId);
            var newOrderDetails = new OrderDetails(openCartDetails.Id.Replace("cd", "od"), newOrderId, openCartDetails.ProductId, product!.Price, openCartDetails.Quantity, discount);
            var discountSystem = new DiscountSystem(new MinimumOrderRules { Quantity = 10 }, new FixDiscount { Rate = discount });
            newOrderDetails.UnitPrice = discountSystem.executeDiscount(_mapper.Map<OrderDetailsStatsDTO>(newOrderDetails));
            await logicOrderDetails.Add(newOrderDetails);
            return _mapper.Map<OrderDetailsDTO>(newOrderDetails);
                        
        }
        




    }
}