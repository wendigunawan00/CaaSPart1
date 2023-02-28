namespace CaaS.Logic
{
    public class PercentageDiscount:IDiscountAction
    {
        public double PercentageRate { get; set; }
        

        public double calculateNewPrice(double Price)
        {
            if(PercentageRate > 1 || PercentageRate<0)
            {
                return Price;
            }
            return Price - PercentageRate * Price;
        }
    }

}