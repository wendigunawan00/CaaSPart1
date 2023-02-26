using AutoMapper;
using CaaS.DTO;
using Microsoft.AspNetCore.Mvc;
using CaaS.Logic;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using CaaS.Domain;

namespace CaaS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(WebApiConventions))]
    public class CartsOrdersController : ControllerBase
    {
        private readonly IOrderManagementLogic orderMgtLogic;
        private IMapper mapper;


        public CartsOrdersController(IOrderManagementLogic orderManagementLogicInstance,IMapper mapper)
        {
            this.orderMgtLogic = orderManagementLogicInstance;
            orderMgtLogic.setMapper(mapper ?? throw new ArgumentNullException(nameof(mapper)));
            this.mapper = mapper;
        }
        /// <summary>
        /// Delete a product with the product-id given
        /// </summary>
        [HttpDelete("{custId}")]
        //[Authorize]
        public async Task<ActionResult> EmptyCartByCustId([FromRoute] string custId)
        {
            var opencart = orderMgtLogic.ShowOpenCartByCustomerID(custId);

            if (opencart is not null) {
                await orderMgtLogic.DeleteCartDetailsByCartId(opencart.Result.Id); 

                return Ok("Finished Deleting");
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Returns all open carts all customers.
        /// <returns> all carts with open status</returns>
        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<CartDTO>> ShowOpenCart()
        {
            return await orderMgtLogic.ShowOpenCart();
        }

        /// <summary>
        /// Returns a Cart Object
        /// </summary>
        /// <returns>an open cart of a customer</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<CartDetailsDTO>> CreateOrUpdateCart(string customerId, string productId, double quantity)
        {
            if(await orderMgtLogic.isValidProduct(productId) is null || await orderMgtLogic.isValidCustomer(customerId)is null)
            {
                return NotFound("Wrong product Id or customer Id");
            }
            var cart = await orderMgtLogic.ShowOpenCartByCustomerID(customerId); // return only 1 element
            if (cart is null ) // if a customer has no open cart then create a new cart for this customer
            {
                await orderMgtLogic.CreateCart(customerId);
                var newCart1 = await orderMgtLogic.ShowOpenCartByCustomerID(customerId);
                var cartDetails= await orderMgtLogic.CreateCartDetails((newCart1).Id, productId, quantity);
                return mapper.Map<CartDetailsDTO>(cartDetails);

            }
            else //if a customer has an open cart then update the cart-details or create a new cart-details
            {
                var cartDetails = await orderMgtLogic.ShowOpenCartDetailsByCartIdAndProductId(cart.Id, productId);

                if (cartDetails is not null)
                {
                    cartDetails.Quantity = quantity;
                    await orderMgtLogic.UpdateCartDetails(cartDetails);
                }
                else
                {
                    cartDetails= await orderMgtLogic.CreateCartDetails(cart.Id, productId, quantity);
                }
                 return mapper.Map<CartDetailsDTO>(cartDetails);
            }
        }

        /// <summary>
        /// Returns an OrderDetailsObject
        /// </summary>
        /// <returns>create an order out of an open cart of a customer</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<OrderDetailsDTO?>> CreateOrder(string customerId, double discount, MinimumOrderRules minOrderRule, FixDiscount fixDiscAction, PeriodeRules periodeRules, PercentageDiscount percDiscAction)
        {
            if (await orderMgtLogic.isValidCustomer(customerId) is null)
            {
                return NotFound("Wrong customer Id");
            }

            var openCart = await orderMgtLogic.ShowOpenCartByCustomerID(customerId);
            var someDiscRules = new List<IDiscountRule>();
            someDiscRules.Add(minOrderRule);
            someDiscRules.Add(periodeRules);
            var someDiscActions = new List<IDiscountAction>();
            someDiscActions.Add(fixDiscAction);
            someDiscActions.Add(percDiscAction);
            if (openCart is not null) // if openCart not empty
            {
                var openCartDetails = (await orderMgtLogic.ShowOpenCartDetailsByCartId((openCart).Id));
                if (openCartDetails.IsNullOrEmpty()) // if openCartDetails is not empty
                {
                    for (int i = 0; i < openCartDetails.Count(); i++)
                    {
                        CartDetailsDTO openCartDetails1 = openCartDetails.ToArray().ElementAt(i);
                        var ok = await orderMgtLogic.CreateOrder((openCart), openCartDetails1, discount, someDiscRules, someDiscActions);
                        if (i == openCartDetails.Count() - 1)
                        {
                            return Ok(ok);
                        }
                    }
                }
            }            
            //return NotFound(StatusInfo.InvalidOrderId((openCart).ToArray().ElementAt(0).Id.Replace("cart", "or")));
            return NotFound("No Open cart for this customer");
            
        }

        

    }
}
