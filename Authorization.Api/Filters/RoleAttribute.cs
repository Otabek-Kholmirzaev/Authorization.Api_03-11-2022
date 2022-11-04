using Microsoft.AspNetCore.Mvc;

namespace Authorization.Api.Filters;

public class RoleAttribute : TypeFilterAttribute
{
    public RoleAttribute(string roles) : base(typeof(AuthorizationAttribute))
    {
        Arguments = new object[] { roles };
    }
}
