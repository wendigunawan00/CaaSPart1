namespace CaaS.DTO;



public class AddressForCreationDTO{   

    public string Street { get; set; }

    public AddressForCreationDTO(string street, string floor, int postalCode, string city, string province, string country)
    {
        Street = street;
        Floor = floor;
        PostalCode = postalCode;
        City = city;
        Province = province;
        Country = country;
    }

    public string Floor { get; set; }
    public int PostalCode { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string Country { get; set; }
    

    public override string ToString() =>
      $"Address(Street:{Street}, Floor{Floor}, PostalCode:{PostalCode}, City:{City},Province:{Province},Country:{Country})";
}
