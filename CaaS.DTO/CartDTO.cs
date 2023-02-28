namespace CaaS.DTO;

public class CartDTO
{
    public string Id { get; set; }

    public CartDTO(string id, string custId, string status)
    {
        Id = id;
        CustId = custId;
        Status = status;
    }

    public string CustId { get; set; }
    public string Status { get; set; }


    public override string ToString() =>
      $"Cart(id:{Id}, CustId:{CustId}, Status:{Status})";
}
