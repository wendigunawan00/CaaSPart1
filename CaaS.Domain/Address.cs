namespace CaaS.Domain;



public class Address
{
    public Address(string id, string street, string floor, int postalCode, string city, string province,string country)
    {
        Id = id;
        Street = street;
        Floor = floor;
        PostalCode = postalCode;
        City = city;
        Province = province;
        Country = country;
    }

    public string Id { get; set; }
    public string Street { get; set; }
    public string Floor { get; set; }
    public int PostalCode { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string Country { get; set; }
    

    public override string ToString() =>
      $"Address(id:{Id}, Street:{Street}, Floor{Floor}, PostalCode:{PostalCode}, City:{City})";
}
