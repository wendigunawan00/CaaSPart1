namespace CaaS.Logic
{
    public class PercentageDiscount:IDiscountAction
    {
        public double Rate { get; set; }
        

        public double calculateNewPrice(double Price)
        {
            if(Rate > 1 || Rate<0)
            {
                return Price;
            }
            return Price - Rate * Price;
        }
    }

}