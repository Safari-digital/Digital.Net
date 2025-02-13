using System.Security.Authentication;
using Digital.Lib.Net.Authentication.Exceptions;
using Digital.Lib.Net.Authentication.Models;
using Digital.Lib.Net.Authentication.Models.Authorizations;
using Digital.Lib.Net.Authentication.Models.Events;
using Digital.Lib.Net.Authentication.Services.Authentication.ApiUsers;
using Digital.Lib.Net.Authentication.Services.Authentication.Events;
using Digital.Lib.Net.Authentication.Services.Authorization;
using Digital.Lib.Net.Authentication.Services.Options;
using Digital.Lib.Net.Authentication.Services.Security;
using Digital.Lib.Net.Core.Messages;
using Digital.Lib.Net.Entities.Models;
using Digital.Lib.Net.Entities.Repositories;
using Digital.Lib.Net.Mvc.Services;
using Microsoft.EntityFrameworkCore;

namespace Digital.Lib.Net.Authentication.Services.Authentication;

public class AuthenticationService<TApiUser, TAuthorization>(
    IHttpContextService httpContextService,
    IJwtOptionService jwtOptions,
    IAuthenticationEventService<TApiUser> authenticationEventService,
    IAuthenticationJwtService authenticationJwtService,
    IAuthorizationJwtService<TApiUser> authorizationJwtService,
    IApiUserService<TApiUser> apiUserService,
    IRepository<TApiUser> apiUserRepository,
    IRepository<TAuthorization> tokenRepository
) : IAuthenticationService<TApiUser>
    where TApiUser : EntityGuid, IApiUser
    where TAuthorization : AuthorizationToken
{
    public async Task<Result<TApiUser>> ValidateCredentials(string login, string password)
    {
        var result = new Result<TApiUser>
        {
            Value = await apiUserRepository.Get(u => u.Login == login).FirstOrDefaultAsync()
        };

        if (result.Value is null)
            result.AddError(new AuthenticationInvalidCredentialsException());
        else if (!result.Value.IsActive)
            result.AddError(new AuthenticationInactiveUserException());
        else if (!HashService.VerifyPassword(result.Value, password))
            result.AddError(new AuthenticationInvalidCredentialsException());

        return result;
    }

    public async Task<Result<string>> Login(string login, string password)
    {
        ApiEventState state;
        var result = new Result<string>();
        var userResult = new Result<TApiUser>();

        if (authenticationEventService.HasTooManyAttempts(login))
        {
            result.AddError(new AuthenticationTooManyAttemptsException());
            state = ApiEventState.Failed;
        }
        else
        {
            userResult = await ValidateCredentials(login, password);
            state = userResult.HasError() ? ApiEventState.Failed : ApiEventState.Success;
        }

        result.Merge(userResult);

        await authenticationEventService.RegisterEventAsync(
            AuthenticationEventType.Login,
            state,
            result,
            userResult.Value?.Id,
            login
        );

        if (result.HasError())
            return result;

        result.Value = authenticationJwtService.GenerateBearerToken(userResult.Value!.Id);
        httpContextService.SetResponseCookie(
            authenticationJwtService.GenerateRefreshToken(userResult.Value.Id),
            jwtOptions.CookieName,
            jwtOptions.GetRefreshTokenExpirationDate()
        );
        return result;
    }

    public Result<string> RefreshTokens()
    {
        var token = httpContextService.Request.Cookies[jwtOptions.CookieName];
        var result = new Result<string>();

        var tokenResult = authorizationJwtService.AuthorizeApiUserRefresh(token);
        result.Merge(tokenResult);
        if (result.HasError())
            return result;

        httpContextService.SetResponseCookie(
            authenticationJwtService.GenerateRefreshToken(tokenResult.ApiUserId),
            jwtOptions.CookieName,
            jwtOptions.GetRefreshTokenExpirationDate()
        );
        result.Value = authenticationJwtService.GenerateBearerToken(tokenResult.ApiUserId);
        return result;
    }

    public async Task<Result> Logout()
    {
        var result = new Result();
        var refreshToken = httpContextService.Request.Cookies[jwtOptions.CookieName];
        if (refreshToken is null)
            return result.AddError(new AuthenticationException());

        await authenticationJwtService.RevokeTokenAsync(refreshToken);
        httpContextService.Response.Cookies.Delete(jwtOptions.CookieName);

        await authenticationEventService.RegisterEventAsync(
            AuthenticationEventType.Logout,
            ApiEventState.Success,
            null,
            apiUserService.GetAuthenticatedUserId()
        );
        return result;
    }

    public async Task<Result> LogoutAll()
    {
        var result = new Result();
        var refreshToken = httpContextService.Request.Cookies[jwtOptions.CookieName];
        var apiUserId = tokenRepository.Get(u => u.Key == refreshToken).FirstOrDefault()?.ApiUserId;
        if (apiUserId is null)
            return result.AddError(new AuthenticationException());

        await authenticationJwtService.RevokeAllTokensAsync(apiUserId.Value);
        httpContextService.Response.Cookies.Delete(jwtOptions.CookieName);

        await authenticationEventService.RegisterEventAsync(
            AuthenticationEventType.LogoutAll,
            ApiEventState.Success,
            null,
            apiUserId
        );
        return result;
    }
}