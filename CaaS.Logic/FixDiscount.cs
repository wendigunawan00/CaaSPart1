namespace CaaS.Logic
{
    public record FixDiscount:IDiscountAction
    {
        public double Rate { get; set; }
        //public double GetRate() { return Rate; }
        //public void SetRate(double discount) { Rate = discount; }
        public double calculateNewPrice(double Price)
        {
            if (Rate > Price || Rate < 0)
            {
                return Price;
            }
            return Price - Rate ;
        }
    }

}