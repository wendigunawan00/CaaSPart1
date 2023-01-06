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
    [Authorize]
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
        /// <param name="startDate">start date</param>
        /// <param name="endDate">end date</param>
        /// <param name="n">end date</param>
        /// <returns>Get n Best Seller Products</returns>      
        [HttpGet("{startDate},{endDate},{n}")]        
        //public async Task<IEnumerable<OrderDetailsStatsDTO>> GetMostPopularItem(DateTime startDate, DateTime endDate)
        public async Task<IEnumerable<BestSellerStatsDTO>> GetBestSellerItem(DateTime startDate, DateTime endDate, int n)
        {
            var orders = await _analytic.GetBestSeller(startDate,endDate,n);           
            return (orders);
        }

        /// <summary>
        /// Returns total revenue within a certain time frame.
        /// </summary>
        /// <param name="startDate">start date</param>
        /// <param name="endDate">end date</param>
        /// <returns>Get total revenue within a certain time frame</returns>      
        [HttpGet("{startDate},{endDate}")]        
        public async Task<RevenueStatsDTO> GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            var orders = await _analytic.GetTotalRevenue(startDate,endDate);           
            return (orders);
        } 
        
        /// <summary>
        /// Returns total revenue within a certain time frame.
        /// </summary>
        /// <param name="startDate">start date</param>
        /// <returns>Get total revenue within a certain time frame</returns>      
        [HttpGet("{startDate}")]        
        public async Task<CartsStatsDTO> GetAllOpenAndClosedCart(DateTime startDate)
        {
            var carts = await _analytic.GetCartsStats(startDate);           
            return carts;
        }
        
        
    }
}
