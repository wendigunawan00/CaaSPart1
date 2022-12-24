namespace CaaS.Dtos;



public class AddressDTO{   

    public string Id { get; set; }
    public string Street { get; set; }
    public string Floor { get; set; }
    public double PostalCode { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string Country { get; set; }
    

    public override string ToString() =>
      $"Address(id:{Id}, Street:{Street}, Floor{Floor}, PostalCode:{PostalCode}, City:{City})";
}
