using CaaS.DTO;

namespace CaaS.Logic
{
    public class MinimumOrderRules:IDiscountRule
    {             
        public int MinimumOrderQuantity{ get; set; }

        public bool isFulfilled(OrderDetailsStatsDTO orderDetails)
        {
            if(orderDetails is null) { return false; }
            if(orderDetails.Quantity >= MinimumOrderQuantity) { return true; }
            return false;

        }
    }

}