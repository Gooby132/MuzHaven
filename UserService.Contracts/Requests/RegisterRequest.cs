namespace UserService.Contracts.Requests;

public class RegisterRequest
{

    public string StageName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Bio { get; set; }
    public string? Image { get; set; }
}
