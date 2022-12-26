namespace CaaS.Domain;

public class Product
{
    public Product(string name, double price, string amountDesc, string productDesc, string downloadLink)
    {       
        this.Name = name;
        this.Price = price;
        this.AmountDesc = amountDesc;
        this.ProductDesc = productDesc;
        this.DownloadLink = downloadLink;
    }

    public Product(string id, string name, double price, string amountDesc, string productDesc, string downloadLink)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
        }

        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrEmpty(amountDesc))
        {
            throw new ArgumentException($"'{nameof(amountDesc)}' cannot be null or empty.", nameof(amountDesc));
        }
       

        Id = id;
        this.Name = name;
        this.Price = price;
        this.AmountDesc = amountDesc;
        this.ProductDesc = productDesc;
        this.DownloadLink = downloadLink;
    }

    

    public string Id { get; set; }
    public string Name { get; set; }
    public double Price{ get; set; }
    public string AmountDesc { get; set; }
    public string? ProductDesc { get; set; }
    public string? DownloadLink { get; set; }

    public override string ToString() =>
      $"Product(id:{Id}, name:{Name}, price:{Price}, amountDesc:{AmountDesc}, productDesc:{ProductDesc})";
}
