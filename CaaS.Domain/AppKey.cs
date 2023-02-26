namespace CaaS.Domain;

public class AppKey
{
    public AppKey(string id, string appKeyName,string appKeyPassword, string shopId)
    {
        Id = id;
        AppKeyName = appKeyName;
        AppKeyPassword = appKeyPassword;
        ShopId = shopId;
    }
    public string Id { get; set; }
    public string AppKeyName { get;  set; }
    public string AppKeyPassword { get;  set; }
    public string ShopId { get; set; }

    public override string ToString() =>
      $"AppKey(id:{Id}, AppKeyName:{AppKeyName},AppKeyPassword:{AppKeyPassword},ShopId:{ShopId})";
}

