using CaaS.DTO;

namespace CaaS.Logic
{
    public class MinimumOrderRules:IDiscountRule
    {             
        public int Quantity{ get; set; }

        public bool isFulfilled(OrderDetailsStatsDTO orderDetails)
        {
            if(orderDetails is null) { return false; }
            if(orderDetails.Quantity >= Quantity) { return true; }
            return false;

        }
    }

}