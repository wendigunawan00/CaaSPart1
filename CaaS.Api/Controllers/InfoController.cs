using AutoMapper;
using Azure;
using CaaS.DTO;
using CaaS.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaaS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [ApiConventionType(typeof(WebApiConventions))]
    public class InfoController : ControllerBase
    {
        private IAnalytic _analytic;

        public InfoController(IAnalytic analytic)
        {
           this._analytic = analytic;      
        }

        /// <summary>
        /// Returns n top-selling products with total sold quantity within a certain time frame.
        /// </summary>
        /// <param name="n">nr of Best seller Product wished</param>
        /// <param name="startDate">start date mm.dd.yyyy</param>
        /// <param name="endDate">end date mm.dd.yyyy</param>
        /// <returns>Get n Best Seller Products</returns>      
        [HttpGet("{n}/{startDate}/{endDate}")]        
        //public async Task<IEnumerable<OrderDetailsStatsDTO>> GetMostPopularItem(DateTime startDate, DateTime endDate)
        public async Task<IEnumerable<BestSellerStatsDTO>> GetBestSellerItem(int n,DateTime startDate, DateTime endDate)
        {
            var orders = await _analytic.GetBestSeller(startDate,endDate,n);           
            return (orders);
        }

        /// <summary>
        /// Returns total revenue within a certain time frame.
        /// </summary>
        /// <param name="startDate">start date mm.dd.yyyy</param>
        /// <param name="endDate">end date mm.dd.yyyy</param>
        /// <returns>Get total revenue within a certain time frame</returns>      
        [HttpGet("{startDate},{endDate}")]        
        public async Task<RevenueStatsDTO> GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            var orders = await _analytic.GetTotalRevenue(startDate,endDate);           
            return (orders);
        } 
        
        /// <summary>
        /// Returns total number of carts within a certain start date.
        /// </summary>
        /// <param name="startDate">start date mm.dd.yyyy</param>
        /// <returns>Get total carts open or closed with a certain start date</returns>      
        [HttpGet("{startDate}")]        
        public async Task<CartsStatsDTO> GetAllOpenAndClosedCart(DateTime startDate)
        {
            var carts = await _analytic.GetCartsStats(startDate);           
            return carts;
        }
        
        
    }
}
