using FluentResults;
using System.Security.Claims;

namespace StemService.Domain.Services;

public interface IFileAuthorizer
{

    public const string StemSchemeName = "StemScheme";

    public const string StemClaimName = "stem-claim";
    public const string StemHeaderName = "stem-claim";
    public Task<Result<string>> GenerateKeyForStems(IEnumerable<Guid> stemsIds, CancellationToken token = default);
    public Task<Result<IEnumerable<Guid>>> ParseAuthorizedKey(ClaimsPrincipal user, CancellationToken token = default);

}
