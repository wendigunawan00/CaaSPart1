namespace CaaS.DTO;

public class BestSellerStatsDTO
{
    public string ProductId { get; set; }
    public double TotalQuantity { get;  set; }

    //public BestSellerStatsDTO( string prodId, double quantity)
    //{
    //    ProductId = prodId;
    //    TotalQuantity = quantity;
    //}
    public override string ToString() =>
      $"BestSellerStats(ProducId:{ProductId}, Quantity:{TotalQuantity})";
}

