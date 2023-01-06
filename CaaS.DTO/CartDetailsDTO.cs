namespace CaaS.DTO;

public class CartDetailsDTO
{
    public string Id { get; set; }
    public string CartId { get; set; }
    public string ProductId { get; set; }
    public double Quantity { get; set; }

    public override string ToString() =>
      $"Cart(Status:{ProductId}, Quantity:{Quantity})";
}
