using AutoMapper;
using CaaS.DTO;
using Microsoft.AspNetCore.Mvc;
using CaaS.Logic;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using CaaS.Domain;
using System;
using System.Globalization;

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
        /// Empty an open cart of customer with given the customer id
        /// </summary>
        [HttpDelete("{custId}")]
        //[Authorize]
        public async Task<ActionResult> EmptyCartByCustId([FromRoute] string custId)
        {
            var opencart = await orderMgtLogic.ShowOpenCartByCustomerID(custId);

            if (opencart.IsNullOrEmpty()) {
                await orderMgtLogic.DeleteCartDetailsByCartId(opencart.ToArray()[0].Id); 

                return Ok("Finished Deleting");
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Returns all open carts all customers.
        /// </summary>
        /// <returns> all carts with open status</returns>
        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<CartDTO>> ShowOpenCart()
        {
            return await orderMgtLogic.ShowOpenCart();
        }

        /// <summary>
        /// Create or update cart
        /// </summary>
        /// <returns>if success, show an open cart of a customer</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<CartDetailsDTO>> CreateOrUpdateCart(string customerId, string productId, double quantity)
        {
            if(await orderMgtLogic.isValidProduct(productId) is null || await orderMgtLogic.isValidCustomer(customerId)is null)
            {
                return NotFound("Wrong product Id or customer Id");
            }
            var cart = (await orderMgtLogic.ShowOpenCartByCustomerID(customerId)); // return only 1 element
            if (cart.IsNullOrEmpty() ) // if a customer has no open cart then create a new cart for this customer
            {
                await orderMgtLogic.CreateCart(customerId);
                var newCart1 = (await orderMgtLogic.ShowOpenCartByCustomerID(customerId)).ToArray()[0];
                var cartDetails= await orderMgtLogic.CreateCartDetails(newCart1!.Id, productId, quantity);
                return mapper.Map<CartDetailsDTO>(cartDetails);

            }
            else //if a customer has an open cart then update the cart-details or create a new cart-details
            {
                var cartDetails = await orderMgtLogic.ShowOpenCartDetailsByCartIdAndProductId(cart.ToArray()[0]!.Id, productId);

                if (cartDetails is not null)
                {
                    cartDetails.Quantity = quantity;
                    await orderMgtLogic.UpdateCartDetails(cartDetails);
                }
                else
                {
                    cartDetails= await orderMgtLogic.CreateCartDetails(cart.ToArray()[0]!.Id, productId, quantity);
                }
                 return mapper.Map<CartDetailsDTO>(cartDetails);
            }
        }

        /// <summary>
        /// Create an Order with their details
        /// </summary>
        /// <returns>create an order out of an open cart of a customer</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<OrderDetailsDTO?>> CreateOrder(string customerId, double discount, [FromBody] DiscountRulesActionsDTO discountsystemDTO)
        {
            if (await orderMgtLogic.isValidCustomer(customerId) is null)
            {
                return NotFound("Wrong customer Id");
            }
            DateTime startDateTime,endDateTime;
            string[] formats = { "MM/dd/yyyy hh:mm:ss tt", "yyyy-MM-dd hh:mm:ss", "dd/MM/yyyy", "dd-MM-yyyy", "dd.MM.yyyy" };
            DateTime.TryParseExact(discountsystemDTO.startDate, formats, new CultureInfo("en-GB"), DateTimeStyles.None, out startDateTime);
            DateTime.TryParseExact(discountsystemDTO.endDate, formats, new CultureInfo("en-GB"), DateTimeStyles.None, out endDateTime);

            var openCart = await orderMgtLogic.ShowOpenCartByCustomerID(customerId);
            var someDiscRules = new List<IDiscountRule>();
            someDiscRules.Add(new MinimumOrderRules { MinimumOrderQuantity = discountsystemDTO.MinimumOrderQuantity }) ;
            someDiscRules.Add(new PeriodeRules { startDate=startDateTime,endDate=endDateTime });
            var someDiscActions = new List<IDiscountAction>();
            someDiscActions.Add(new FixDiscount { FixRate = discountsystemDTO.FixRate});
            someDiscActions.Add(new PercentageDiscount { PercentageRate= discountsystemDTO.PercentageRate});
            if (!openCart.IsNullOrEmpty()) // if openCart not empty
            {
                var openCartDetails = (await orderMgtLogic.ShowOpenCartDetailsByCartId((openCart.ToArray()[0]!).Id));
                if (!openCartDetails.IsNullOrEmpty()) // if openCartDetails is not empty
                {
                    for (int i = 0; i < openCartDetails.Count(); i++)
                    {
                        CartDetailsDTO openCartDetails1 = openCartDetails.ToArray().ElementAt(i)!;
                        var ok = await orderMgtLogic.CreateOrder(openCart.ToArray()[0]!, openCartDetails1, discount, someDiscRules, someDiscActions);
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
