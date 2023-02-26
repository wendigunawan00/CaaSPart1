namespace CaaS.DTO;



public class OrderDTO
{    
    public string Id { get; set; }
    public string CustId { get; private set; }
    public string CartId { get; private set; }
    public DateTime OrderDate { get; set; }



    public override string ToString() =>
      $"Order(id:{Id}, CustId:{CustId}, CartId{CartId}, OrderDate:{OrderDate:yyyy-MM-dd})";
}
