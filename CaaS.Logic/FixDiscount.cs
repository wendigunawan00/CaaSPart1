namespace CaaS.Logic
{
    public record FixDiscount:IDiscountAction
    {
        public double FixRate { get; set; }
        //public double GetRate() { return Rate; }
        //public void SetRate(double discount) { Rate = discount; }
        public double calculateNewPrice(double Price)
        {
            if (FixRate > Price || FixRate < 0)
            {
                return Price;
            }
            return Price - FixRate ;
        }
    }

}