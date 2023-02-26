namespace CaaS.DTO;

public class RevenueStatsDTO
{
    public double TotalRevenue { get; set; }

    public RevenueStatsDTO(double totalRevenue, double totalQuantity)
    {
        TotalRevenue = totalRevenue;
        TotalQuantity = totalQuantity;
    }

    public double TotalQuantity { get;  set; }

    
    public override string ToString() =>
      $"SalesFiguresStats(TotalRevenue:{TotalRevenue}, Quantity:{TotalQuantity})";
}

