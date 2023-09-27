using PermissionService.Infrastructure.Authorization.Core;

namespace PermissionService.Infrastructure.Authorization.Abstracts;

public interface IAuthorizationTokenProvider
{
    public Token CreateGuestToken(Guid userId);
    public Token CreateReaderToken(Guid userId, Guid project);
    public Token CreateCommenterToken(Guid userId, Guid project);
    public Token CreateContributerToken(Guid userId, Guid project);

}
