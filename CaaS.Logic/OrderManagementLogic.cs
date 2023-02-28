using AutoMapper;
using CaaS.DTO;
using CaaS.Domain;
using CaaS.Dal.Ado;
using CaaS.Dal.Interfaces;
using MySqlX.XDevAPI.Relational;
using System.Collections.Generic;
using System.Reflection;

namespace CaaS.Logic
{
    public class OrderManagementLogic :IOrderManagementLogic
    {
        private readonly AdoCartDao logicCart;
        private readonly AdoOrderDao logicOrder;
        private readonly AdoProductDao logicProduct;
        private readonly AdoCartDetailsDao logicCartDetails;
        private readonly AdoOrderDetailsDao logicOrderDetails;
        private readonly AdoPersonDao logicPerson;
        private readonly string[] cartOrderProductCartDetOrderDetPerson;
        private IMapper _mapper;



        public OrderManagementLogic(AdoCartDao logicCart, AdoOrderDao logicOrder,
            AdoProductDao logicProduct,AdoCartDetailsDao logicCartDetails, AdoOrderDetailsDao logicOrderDetails,
            AdoPersonDao logicPerson,string[] CartOrderProductCartDetOrderDetPerson
            )
        {
            this.logicCart = logicCart ?? throw new ArgumentNullException(nameof(logicCart));
            this.logicOrder = logicOrder ?? throw new ArgumentNullException(nameof(logicOrder));
            this.logicProduct = logicProduct ?? throw new ArgumentNullException(nameof(logicProduct));
            this.logicCartDetails = logicCartDetails ?? throw new ArgumentNullException(nameof(logicCartDetails));
            this.logicOrderDetails = logicOrderDetails ?? throw new ArgumentNullException(nameof(logicOrderDetails));
            this.logicPerson = logicPerson;
            this.cartOrderProductCartDetOrderDetPerson = CartOrderProductCartDetOrderDetPerson;
        }
                

        public void setMapper(IMapper value)
        {
            _mapper = value;
        }
        
        public async Task<ProductDTO?> isValidProduct(string productId)
        {
            return _mapper.Map<ProductDTO>(await logicProduct.FindByIdAsync(productId, cartOrderProductCartDetOrderDetPerson[2]));
        }
        public async Task<PersonDTO?> isValidCustomer(string custId)
        {
            return _mapper.Map<PersonDTO>(await logicPerson.FindByIdAsync(custId, cartOrderProductCartDetOrderDetPerson[5]));
        }
        public async Task<IEnumerable<CartDTO?>> ShowOpenCart()
        {
            var carts = await logicCart.FindTByStatus("open", cartOrderProductCartDetOrderDetPerson[0]);
            return _mapper.Map <IEnumerable <CartDTO>>( carts )?? Enumerable.Empty<CartDTO>();
        }

        public async Task<IEnumerable<CartDTO?>> ShowOpenCartByCustomerID(string customerId)
        {
            var carts = (await logicCart.FindTByXAndY("open",customerId, cartOrderProductCartDetOrderDetPerson[0]));            
            return _mapper.Map<IEnumerable<CartDTO?>>(carts) ;
        }
        
        public async Task<CartDetailsDTO?> ShowOpenCartDetailsByCartIdAndProductId(string openCartId, string productId)
        {
            var foundCartDetails = await logicCartDetails.FindOpenCartDetailsByCartIdAndProductId(openCartId, productId, cartOrderProductCartDetOrderDetPerson[3]);
            return _mapper.Map<CartDetailsDTO?>(foundCartDetails);
        } 
        
        public async Task<IEnumerable<CartDetailsDTO?>> ShowOpenCartDetailsByCartId(string openCartId)
        {
            var foundCartDetails = await logicCartDetails.FindOpenCartDetailsByCartId(openCartId, cartOrderProductCartDetOrderDetPerson[3]);
            return _mapper.Map<IEnumerable<CartDetailsDTO>>(foundCartDetails);
        }

        public async Task<CartDTO> CreateCart(string customerId)
        {             
            return _mapper.Map<CartDTO>(await logicCart.CreateCart(customerId, cartOrderProductCartDetOrderDetPerson[0]));            
        }

        public async Task<CartDetailsDTO> CreateCartDetails(string openCartId, string productId, double quantity)
        {   
            return _mapper.Map<CartDetailsDTO>(await logicCartDetails.CreateCartDetails(openCartId, productId, quantity, cartOrderProductCartDetOrderDetPerson[3]));
        } 
        
        public async Task<bool> DeleteCartDetailsByCartId(string cartId)
        {   
            return (await logicCartDetails.DeleteCartDetailsByCartId(cartId, cartOrderProductCartDetOrderDetPerson[3]));
        }
               
        public async Task<bool> UpdateCartDetails(CartDetailsDTO cartDetailsToBeUpdated)
        {
            return await logicCartDetails.UpdateAsync(_mapper.Map<CartDetails>(cartDetailsToBeUpdated), cartOrderProductCartDetOrderDetPerson[3]);
        }                

        public async Task<OrderDetailsDTO> CreateOrder(CartDTO openCart, CartDetailsDTO openCartDetails, double discount, List<IDiscountRule> discountRules, List<IDiscountAction> discountActions )
        {            
            openCart.Status = "closed";
            var newOrderId = openCart.Id.Replace("cart", "ord");
            await logicCart.UpdateAsync(_mapper.Map<Cart>(openCart), cartOrderProductCartDetOrderDetPerson[0]);
            var newOrder = new Order(newOrderId, openCart.CustId, openCart.Id, DateTime.Now);
            await logicOrder.StoreAsync(newOrder, cartOrderProductCartDetOrderDetPerson[1]);
            var product = await logicProduct.FindByIdAsync(openCartDetails.ProductId, cartOrderProductCartDetOrderDetPerson[2]);
            var newOrderDetails = new OrderDetails(openCartDetails.Id.Replace("cart", "ord"), newOrderId, openCartDetails.ProductId,
                product!.Price, openCartDetails.Quantity, discount,product.ShopId);
            var discountSystem = new DiscountSystem(discountRules,discountActions);
            
            var newUnitPrice = discountSystem.executeDiscount(new OrderDetailsStatsDTO(newOrderDetails.Id,
                newOrderDetails.OrderId,newOrderDetails.ProductId,newOrderDetails.UnitPrice,newOrderDetails.Quantity,
                newOrderDetails.Discount,openCart.CustId,openCart.Id,DateTime.Now));
            newOrderDetails.Discount = newOrderDetails.UnitPrice - newUnitPrice;
            newOrderDetails.UnitPrice = newUnitPrice;
            await logicOrderDetails.StoreAsync(newOrderDetails, cartOrderProductCartDetOrderDetPerson[4]);
            return _mapper.Map<OrderDetailsDTO>(newOrderDetails);                        
        }
        




    }
}