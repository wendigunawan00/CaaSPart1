namespace CaaS.DTO;

public class DiscountRulesDTO
{
    public int MinimumOrderQuantity { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }

    public override string ToString() =>
      $"CartDetails(startDate:{startDate}, endDate:{endDate}, Quantity:{MinimumOrderQuantity})";
}
