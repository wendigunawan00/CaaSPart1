namespace CaaS.Domain;



public class OrderDetails
{
    public OrderDetails(string id, string orderId, string productId, double unitPrice, double quantity, double discount)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
        Discount = discount;        
    }

    public string Id { get; set; }
    public string OrderId { get; private set; }
    public string ProductId { get; private set; }
    public double UnitPrice { get; set; }
    public double Quantity { get; set; }
    public double Discount { get; set; }
    

    public override string ToString() =>
      $"OrdersDetails(id:{Id}, OrderId:{OrderId}, ProductId{ProductId}, UnitPrice:{UnitPrice}, Quantity:{Quantity})";
}
