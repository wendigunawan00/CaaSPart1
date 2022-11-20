namespace CaaS.Domain;

public class Shop
{
    public Shop(string id, string name,string fieldDesc, string mandantId, string address, string appKey)
    {
        Id = id;
        this.Name = name;
        this.FieldDesc = fieldDesc;
        this.MandantId = mandantId;
        this.Address = address;
        this.AppKey = appKey;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string FieldDesc{ get; set; }
    public string MandantId { get; set; }
    public string Address { get;  set; }
    public string AppKey { get;  set; }

    public override string ToString() =>
      $"Shop(id:{Id}, name:{Name}, mandant:{MandantId})";
}
