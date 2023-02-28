namespace CaaS.DTO;


public class OrderDetailsDTO
{
    public string Id { get; set; }

    public OrderDetailsDTO(string id, string orderId, string productId, double unitPrice, double quantity,double discount, string shopId)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        UnitPrice = unitPrice;
        Discount = discount;
        ShopId = shopId;
        Quantity = quantity;
    }

    public string OrderId { get; private set; }
    public string ProductId { get; private set; }
    public double UnitPrice { get; set; }
    public double Quantity { get; set; }
    public double Discount { get; set; }
    public string ShopId { get; set; }



    public override string ToString() =>
      $"OrdersDetails(id:{Id}, OrderId:{OrderId}, ProductId:{ProductId}, " +
     $"UnitPrice:{UnitPrice}, Quantity:{Quantity}, Discount:{Discount},Shop:{ShopId})";
}
