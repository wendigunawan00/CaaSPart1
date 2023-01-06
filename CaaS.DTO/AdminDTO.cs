namespace CaaS.DTO;

public class AdminDTO
{
    //public string Id { get; set; }
    public string Email { get; set; }

    //public string AppKeyId { get;  set; }
    public string AppKeyName { get;  set; }
    //public string AppKeyPassword { get;  set; }

    public override string ToString() =>
      $"Admin(email:{Email}, AppKeyName:{AppKeyName})";
}

