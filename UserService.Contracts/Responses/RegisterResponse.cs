using UserService.Contracts.Dtos;

namespace UserService.Contracts.Responses;

public class RegisterResponse
{

    public UserDto User { get; set; }

    public string Token { get; set; }

}
