namespace CaaS.DTO;

public class RevenueStatsDTO
{
    public double TotalRevenue { get; set; }
    public double TotalQuantity { get;  set; }

    
    public override string ToString() =>
      $"SalesFiguresStats(TotalRevenue:{TotalRevenue}, Quantity:{TotalQuantity})";
}

