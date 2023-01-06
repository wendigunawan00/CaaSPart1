namespace CaaS.DTO;

public class ProductForCreationDTO
{
    public string Name { get; set; }
    public double Price{ get; set; }
    public string AmountDesc { get; set; }
    public string? ProductDesc { get; set; }
    public string? DownloadLink { get; set; }

    public override string ToString() =>
      $"Product( name:{Name}, price:{Price}, amountDesc:{AmountDesc}, productDesc:{ProductDesc})";
}
