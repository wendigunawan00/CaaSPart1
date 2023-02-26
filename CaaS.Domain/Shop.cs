namespace CaaS.Domain;

public class Shop
{
    public Shop(string id, string name,string fieldDesc, string address)
    {
        Id = id;
        this.Name = name;
        this.FieldDesc = fieldDesc;
        this.Address = address;       
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string FieldDesc{ get; set; }
    public string Address { get;  set; }

    public override string ToString() =>
      $"Shop(id:{Id}, name:{Name},description:{FieldDesc})";
}
