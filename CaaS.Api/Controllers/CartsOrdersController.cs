using AutoMapper;
using CaaS.DTO;
using Microsoft.AspNetCore.Mvc;
using CaaS.Logic;
using CaaS.Features;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

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
        /// Returns all open carts all customers.
        /// <returns> all carts with open status</returns>
        [HttpGet]
        [Authorize]
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
        public async Task<ActionResult<CartDTO>> CreateOrUpdateCart(string customerId, string productId, double quantity)
        {
            var cart = await orderMgtLogic.ShowOpenCartByCustomerID(customerId);
            
            if (cart.IsNullOrEmpty())
            {
                await orderMgtLogic.CreateCart(customerId);
                var newCart = await orderMgtLogic.ShowOpenCartByCustomerID(customerId);
                await orderMgtLogic.CreateCartDetails((newCart).ToArray().ElementAt(0).Id, productId, quantity);
                return mapper.Map<CartDTO>((newCart).ToArray().ElementAt(0));

            }
            else //updating
            {
                var openCart = (cart).ToArray().ElementAt(0);
                var cartDetails = await orderMgtLogic.ShowOpenCartDetailsByProductId(openCart.Id, productId);
                var openCartDetails = (cartDetails).ToArray().ElementAt(0);

                if (cartDetails.IsNullOrEmpty())
                {
                    openCartDetails!.Quantity = quantity;
                    await orderMgtLogic.UpdateCartDetails(openCartDetails);
                }
                else
                {
                    await orderMgtLogic.CreateCartDetails(openCartDetails.Id, productId, quantity);
                }
                 return mapper.Map<CartDTO>(openCartDetails);
            }
        }

        /// <summary>
        /// Returns an OrderDetailsObject
        /// </summary>
        /// <returns>create an order out of an open cart of a customer</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<OrderDetailsDTO?>> CreateOrder(string customerId, double discount)
        {
            var openCart = await orderMgtLogic.ShowOpenCartByCustomerID(customerId);
           
            if (!openCart.IsNullOrEmpty())
            {
            var openCartDetails = (await orderMgtLogic.ShowOpenCartDetailsByCartId((openCart).ToArray().ElementAt(0).Id));
                for (int i = 0; i < openCartDetails.Count(); i++)
                {
                    CartDetailsDTO? openCartDetails1 = openCartDetails!.ToArray().ElementAt(i);
                    var ok = await orderMgtLogic.CreateOrder((openCart).ToArray().ElementAt(0), openCartDetails1, discount);
                    if (i == openCartDetails.Count()-1)
                    {
                        return Ok(ok);
                    }
                }
            }            
            return NotFound(StatusInfo.InvalidOrderId((openCart).ToArray().ElementAt(0).Id.Replace("cart", "or")));
            
        }

        

    }
}
