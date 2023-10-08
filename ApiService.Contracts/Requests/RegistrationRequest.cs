using Microsoft.AspNetCore.Http;

namespace ApiService.Contracts.Requests;

public class RegistrationRequest
{
    public string StageName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Bio { get; set; }
    public string Password { get; set; }


}
