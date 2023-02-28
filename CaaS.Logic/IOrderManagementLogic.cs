using AutoMapper;
using CaaS.DTO;

namespace CaaS.Logic
{
    public interface IOrderManagementLogic 
    {
        public void setMapper(IMapper mapper);
        public Task<ProductDTO?> isValidProduct(string productId);
        public Task<PersonDTO?> isValidCustomer(string custId);
        public Task<IEnumerable<CartDTO?>> ShowOpenCart();
        public Task<IEnumerable<CartDTO?>> ShowOpenCartByCustomerID(string customerId);
        public Task<CartDetailsDTO?> ShowOpenCartDetailsByCartIdAndProductId(string openCartId, string productId);
        public Task<IEnumerable<CartDetailsDTO?>> ShowOpenCartDetailsByCartId(string openCartId);
        public Task<CartDTO> CreateCart(string customerId);
        public Task<bool> DeleteCartDetailsByCartId(string cartId);

        public Task<CartDetailsDTO> CreateCartDetails(string openCartId, string productId, double quantity);
        public Task<bool> UpdateCartDetails(CartDetailsDTO openCartDetails);
        public Task<OrderDetailsDTO> CreateOrder(CartDTO openCart, CartDetailsDTO openCartDetails, double discount,
            List<IDiscountRule> discountRules, List<IDiscountAction> discountActions);      

    }
}