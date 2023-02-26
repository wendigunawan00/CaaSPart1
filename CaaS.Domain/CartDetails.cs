namespace CaaS.Domain;

public class CartDetails
{
    public CartDetails(string id, string cartId, string productId, double quantity,string shopId)
    {
        Id = id;
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
        ShopId = shopId;
    }

    public string Id { get; set; }
    public string CartId { get; set; }
    public string ProductId { get; set; }
    public double Quantity { get; set; }
    public string ShopId { get; set; }

    public override string ToString() =>
      $"Cart(id:{Id}, CartId:{CartId}, ProductId:{ProductId}, Quantity:{Quantity}, Shop:{ShopId})";
}
