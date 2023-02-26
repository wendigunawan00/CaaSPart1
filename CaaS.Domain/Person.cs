namespace CaaS.Domain;

public class Person
{
    public Person(string id, string firstName, string lastName, DateTime dateOfBirth ,string email, string addressId, string status, string password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        AddressId = addressId;
        Status = status;
        Password = password;
    }

    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string AddressId { get; set; }
    public string Status { get; set; }
    public string Password { get; set; }

    public override string ToString() =>
      $"Person(id:{Id}, FirstName:{FirstName}, LastName:{LastName}, DateOfBirth:{DateOfBirth:yyyy-MM-dd}" +
        $"Email:{Email}, Address:{AddressId}, Status:{Status}, Password:{Password})";
}
