using CaaS.DTO;

namespace CaaS.Logic
{
    public class DiscountSystem
    { 
        private readonly IDiscountRule Rule;
        private readonly IDiscountAction Action;

        public DiscountSystem(IDiscountRule rule, IDiscountAction action)
        {
            this.Rule = rule; 
            this.Action = action;
            
        }
                

        public double executeDiscount(OrderDetailsStatsDTO orderDetails)
        {
            if (this.Rule.isFulfilled(orderDetails))
            {
               return this.Action.calculateNewPrice(orderDetails.UnitPrice);
            }
            return orderDetails.UnitPrice; 
        }
    }

}