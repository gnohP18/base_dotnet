using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using static base_dotnet.Common.Enum;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.Items["User"];
        var accountStatus = Convert.ToInt32( context.HttpContext.Items["AccountStatus"]);
        if (user == null)
        {
            // not logged in
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        else if (accountStatus == ((int)AccountStatus.InActive))
            context.Result = new JsonResult(new { message = "Account is not verified or banned" }) { StatusCode = StatusCodes.Status403Forbidden };
    }
}
