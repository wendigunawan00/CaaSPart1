namespace CaaS.DTO;

public class PersonDTO
{
    public PersonDTO(string id, string firstName, string lastName, DateTime dateOfBirth, string email, string addressId, string status)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        AddressId = addressId;
        Status = status;
    }
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string AddressId { get; set;  }
    public string Status { get; }

    public override string ToString() =>
      $"Person(id:{Id}, FirstName:{FirstName}, LastName:{LastName}, DateOfBirth:{DateOfBirth:yyyy-MM-dd}" +
        $"Email:{Email}, Address:{AddressId}), Status:{Status}";
}
