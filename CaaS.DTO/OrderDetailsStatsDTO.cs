namespace CaaS.DTO;



public class OrderDetailsStatsDTO
{       
    public OrderDetailsStatsDTO(string id, string orderId, string productId, double unitPrice, double quantity,
        double discount, string custId, string cartId, DateTime date)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
        Discount = discount;
        CustId = custId;
        CartId = cartId;
        OrderDate = date;
    }

    public string Id { get; set; }
    public string OrderId { get; private set; }
    public string ProductId { get; private set; }
    public double UnitPrice { get; set; }
    public double Quantity { get; set; }
    public double Discount { get; set; }
    public string CustId { get; private set; }
    public string CartId { get; private set; }
    public DateTime OrderDate { get; set; }


    public override string ToString() =>
      $"OrdersDetails(id:{Id}, OrderId:{OrderId}, ProductId:{ProductId}, UnitPrice:{UnitPrice}, Quantity:{Quantity}, OrderDate:{OrderDate})";
}
