namespace UserService.Contracts.Dtos;

public class UserDto
{

    public Guid Id { get; init; }
    public string StageName { get; init; }
    public string? Bio { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
}
