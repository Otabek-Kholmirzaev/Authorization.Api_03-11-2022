using Authorization.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace Authorization.Api.Filters;

public class AuthorizationAttribute : IActionFilter
{
    private readonly UsersService _usersService;
    private readonly string Roles;

    public AuthorizationAttribute(UsersService usersService, string roles)
    {
        _usersService = usersService;
        Roles = roles;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var token = context.HttpContext.Request.Headers[HeaderNames.Authorization];
        
        var userEntity = _usersService.GetUserByToken(token);

        if (userEntity == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!userEntity.Roles.Contains(Roles))
        {
            context.Result = new JsonResult(new { Error = "Access denied" });
            return;
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userEntity.Name),
            new Claim(ClaimTypes.Email, userEntity.Email),
            new Claim(ClaimTypes.Role, userEntity.Roles)
        };

        var identity = new ClaimsIdentity(claims);

        var principal = new ClaimsPrincipal(identity);

        context.HttpContext.User = principal;

    }

    public void OnActionExecuted(ActionExecutedContext context)
    {

    }
}
