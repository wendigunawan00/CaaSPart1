namespace CaaS.Domain;



public class OrderDetails
{
    public OrderDetails(string id, string orderId, string productId, double unitPrice, double quantity, double discount,string shopId)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
        Discount = discount;
        ShopId = shopId;
    }

    public OrderDetails(OrderDetails secondOrder)
    {
        Id = secondOrder.Id;
        OrderId = secondOrder.OrderId;
        ProductId = secondOrder.ProductId;
        UnitPrice = secondOrder.UnitPrice;
        Quantity = secondOrder.Quantity;
        Discount = secondOrder.Discount;
    }

    public string Id { get; set; }
    public string OrderId { get; private set; }
    public string ProductId { get; private set; }
    public double UnitPrice { get; set; }
    public double Quantity { get; set; }
    public double Discount { get; set; }
    public string ShopId { get; set; }

    public override string ToString() =>
      $"OrdersDetails(id:{Id}, OrderId:{OrderId}, ProductId{ProductId}," +
     $"UnitPrice:{UnitPrice}, Quantity:{Quantity}, Discount:{Discount}, Shop:{ShopId})";
}
