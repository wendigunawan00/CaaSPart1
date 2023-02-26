namespace CaaS.DTO;

public class AppKeyDTO
{
    public string Id { get; set; }
    public string AppKeyName { get;  set; }
    public string ShopId { get;  set; }

    public override string ToString() =>
      $"AppKey(id:{Id}, CustId:{AppKeyName})";
}

