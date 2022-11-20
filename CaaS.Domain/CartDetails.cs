namespace CaaS.Domain;

public class CartDetails
{
    public CartDetails(string id, string cartId, string productId, double quantity)
    {
        Id = id;
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
    }

    public string Id { get; set; }
    public string CartId { get; set; }
    public string ProductId { get; set; }
    public double Quantity { get; set; }

    public override string ToString() =>
      $"Cart(id:{Id}, CartId:{CartId}, Status:{ProductId})";
}
