namespace CaaS.DTO;

public class CartForCreationDTO
{
    public string CustId { get; set; }
    public string Status { get; set; }


    public override string ToString() =>
      $"Cart( CustId:{CustId}, Status:{Status})";
}
