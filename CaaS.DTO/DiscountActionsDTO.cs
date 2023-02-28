namespace CaaS.DTO;

public class DiscountRulesActionsDTO
{   
    public int MinimumOrderQuantity { get; set; }
    public string startDate { get; set; }
    public string endDate { get; set; }
    public double FixRate { get; set; }
    public double PercentageRate { get; set; }

    public override string ToString() =>
      $"CartDetails(inimumOrderQuantity:{MinimumOrderQuantity},startDate:{startDate}," +
        $"endDate:{endDate},FixRate:{FixRate}, PercentageRate:{PercentageRate})";
}
