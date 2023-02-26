namespace CaaS.DTO;

public class CartForCreationDTO
{
    public string CustId { get; set; }
    public string Status { get; set; }
    public string ShopId { get; }



    public override string ToString() =>
      $"Cart( CustId:{CustId}, Status:{Status}, Shop:{ShopId})";
}
