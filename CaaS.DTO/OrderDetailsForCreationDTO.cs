namespace CaaS.DTO;

public class OrderDetailsForCreationDTO
{
    public string Id { get; set; }
    public string OrderId { get; private set; }
    public string ProductId { get; private set; }
    public double Quantity { get; set; }
    public double Discount { get; set; }
    public string ShopId { get; }


    public override string ToString() =>
      $"OrdersDetails(id:{Id}, OrderId:{OrderId}, ProductId{ProductId}, Quantity:{Quantity},Shop:{ShopId})";
}
