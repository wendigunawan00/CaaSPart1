namespace CaaS.DTO;

public class PersonAddressForUpdateDTO
{
    public PersonAddressForUpdateDTO(string firstName, string lastName, DateTime dateOfBirth, string email, string status, string password, string floor, int postalCode, string city, string country, string province)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        Status = status;
        Password = password;
        Floor = floor;
        PostalCode = postalCode;
        City = city;
        Country = country;
        Province = province;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public string Password { get; set; }
    public string Street { get; set; }

    public string Floor { get; set; }
    public int PostalCode { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string Country { get; set; }


    public override string ToString() =>
      $"Person(FirstName:{FirstName}, LastName:{LastName}, DateOfBirth:{DateOfBirth:yyyy-MM-dd}" +
        $"Email:{Email}), Status:{Status},Password:{Password}, Address(Street:{Street}, Floor{Floor}, PostalCode:{PostalCode}, City:{City},Province:{Province},Country:{Country}";
}
