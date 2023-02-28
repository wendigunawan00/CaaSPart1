using CaaS.Domain;
using CaaS.DTO;

namespace CaaS.Logic
{
    delegate bool isRuleFulfilled(OrderDetailsStatsDTO orderDetails);
    public class DiscountSystem
    { 
        private readonly List<IDiscountRule> Rule;
        private readonly List<IDiscountAction> Action;


        public DiscountSystem(List<IDiscountRule> rule, List<IDiscountAction> action)
        {
            this.Rule = rule; 
            this.Action = action;
            
        }
                

        public double executeDiscount(OrderDetailsStatsDTO orderDetails)
        {
            double sum = orderDetails.UnitPrice;
            double newPrice = 0;
            for (int i = 0; i < Rule.Count; i++)
            {
                if (this.Rule[i].isFulfilled(orderDetails))
                {
                    newPrice= this.Action[i].calculateNewPrice(sum);
                    sum = newPrice;
                }
            }
            return sum; 
        }
    }

}