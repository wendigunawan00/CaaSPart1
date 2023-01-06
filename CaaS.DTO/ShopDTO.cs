namespace CaaS.DTO;

public class ShopDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string FieldDesc{ get; set; }
    public string MandantId { get; set; }
    public string Address { get;  set; }
    public string AppKey { get;  set; }

    public override string ToString() =>
      $"Shop(id:{Id}, name:{Name}, mandant:{MandantId}),appKey:{AppKey}";
}
