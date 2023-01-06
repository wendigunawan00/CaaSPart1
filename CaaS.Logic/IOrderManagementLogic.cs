using AutoMapper;
using CaaS.DTO;

namespace CaaS.Logic
{
    public interface IOrderManagementLogic 
    {
        public void setMapper(IMapper mapper);

        public Task<IEnumerable<CartDTO>> ShowOpenCart();
        public Task<IEnumerable<CartDTO>> ShowOpenCartByCustomerID(string customerId);
        public Task<IEnumerable<CartDetailsDTO?>> ShowOpenCartDetailsByProductId(string openCartId, string productId);
        public Task<IEnumerable<CartDetailsDTO?>> ShowOpenCartDetailsByCartId(string openCartId);
        public Task<CartDTO> CreateCart(string customerId);

        public Task<CartDetailsDTO> CreateCartDetails(string openCartId, string productId, double quantity);
        public Task<CartDetailsDTO> UpdateCartDetails(CartDetailsDTO openCartDetails);
        public Task<OrderDetailsDTO> CreateOrder(CartDTO openCart, CartDetailsDTO openCartDetails, double discount);      

    }
}