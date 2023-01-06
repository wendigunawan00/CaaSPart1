using CaaS.DTO;
using AutoMapper;


namespace CaaS.Logic
{
    public interface IDiscountRule
    {
        public bool isFulfilled(OrderDetailsStatsDTO orderDetails);
    }
}
