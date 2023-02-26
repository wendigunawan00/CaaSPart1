using System.Data;

namespace CaaS.DTO;

public class BestSellerStatsDTO
{

    public BestSellerStatsDTO(string id, double totalQuantity)
    {
        this.Id = id;
        this.TotalQuantity = totalQuantity;
    }

    public string Id { get; set; }
    public double TotalQuantity { get; set; }


    public override string ToString() =>
      $"BestSellerStats(ProductId:{Id}, Quantity:{TotalQuantity})";
}