namespace CaaS.DTO;

public class PersonDTO
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; }
    public string AddressId { get; }
    public string Status { get; }

    public override string ToString() =>
      $"Person(id:{Id}, FirstName:{FirstName}, LastName:{LastName}, DateOfBirth:{DateOfBirth:yyyy-MM-dd})";
}
