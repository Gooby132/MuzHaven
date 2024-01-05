using UserService.Contracts.Dtos;

namespace UserService.Contracts.Responses;

public class GetAllUsersResponse
{

    public IEnumerable<UserDto> Users { get; set; }

}
