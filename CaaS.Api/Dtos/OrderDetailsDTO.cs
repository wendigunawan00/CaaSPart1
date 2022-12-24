namespace CaaS.Dtos;



public class OrderDetailsDTO
{
    public string Id { get; set; }
    public string OrderId { get; private set; }
    public string ProductId { get; private set; }
    public double UnitPrice { get; set; }
    public double Quantity { get; set; }
    public double Discount { get; set; }
    

    public override string ToString() =>
      $"OrdersDetails(id:{Id}, OrderId:{OrderId}, ProductId{ProductId}, UnitPrice:{UnitPrice}, Quantity:{Quantity})";
}
