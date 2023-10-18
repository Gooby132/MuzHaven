﻿using PermissionService.Infrastructure.Authorization.Core;

namespace PermissionService.Infrastructure.Authorization.Abstracts;

public interface IAuthorizationTokenProvider
{

    public const string PermissionTypeClaim = "type";
    public const string UserIdClaim = "userId";

    public Token CreateGuestToken(string userId);
    public Token CreateReaderToken(string userId, Guid project);
    public Token CreateCommenterToken(string userId, Guid project);
    public Token CreateContributerToken(string userId, Guid project);

}
