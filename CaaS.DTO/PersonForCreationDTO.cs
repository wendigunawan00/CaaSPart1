namespace CaaS.DTO;

public class PersonForCreationDTO
{
    public PersonForCreationDTO(string firstName, string lastName, DateTime dateOfBirth, string email, string address, string status,string password)
    {        
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        AddressId = address;
        Status = status;
        Password = password;
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string AddressId { get; set; }
    public string Status { get; set; }
    public string Password { get; set; }

    public override string ToString() =>
      $"Person(FirstName:{FirstName}, LastName:{LastName}, DateOfBirth:{DateOfBirth:yyyy-MM-dd}" +
        $"Email:{Email}, AddressId:{AddressId}, Status:{Status}, Password:{Password}";
}
