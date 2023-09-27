namespace UserService.Contracts.Requests;

public class RegisterUserRequest
{

    public string Name { get; set; }
    public string Bio { get; set; }
    public string? Image { get; set; }

}
