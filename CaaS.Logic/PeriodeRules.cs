using CaaS.DTO;

namespace CaaS.Logic
{
    public class PeriodeRules:IDiscountRule
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public bool isFulfilled(OrderDetailsStatsDTO orderDetails)
        {
            if (orderDetails is null) { return false; }
            if (orderDetails.OrderDate>startDate && orderDetails.OrderDate < endDate) { return true; }
            return false;
        }
    }

}