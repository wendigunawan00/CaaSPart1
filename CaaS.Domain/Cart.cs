namespace CaaS.Domain;

public class Cart
{
    public Cart(string id, string custId, string status)
    {
        Id = id;
        CustId = custId;
        Status = status;
    }

    public string Id { get; private set; }
    public string CustId { get; set; }
    public string Status { get; set; }


    public override string ToString() =>
      $"Cart(id:{Id}, CustId:{CustId}, Status:{Status})";
}
