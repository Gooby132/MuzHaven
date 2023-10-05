using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ApiService.Contracts.Requests;

public class RegistrationRequest
{
    [Required]
    public string StageName { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Bio { get; set; }
    public IFormFile Profile { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }


}
