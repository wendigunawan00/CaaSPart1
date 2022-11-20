namespace CaaS.Domain;

public class AppKey
{
    public AppKey(string id, string appKeyName,string appKeyPassword)
    {
        Id = id;
        AppKeyName = appKeyName;
        AppKeyPassword = appKeyPassword;

    }
    public string Id { get; set; }
    public string AppKeyName { get;  set; }
    public string AppKeyPassword { get;  set; }

    public override string ToString() =>
      $"AppKey(id:{Id}, CustId:{AppKeyName})";
}

