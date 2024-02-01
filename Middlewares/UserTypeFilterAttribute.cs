using base_dotnet.Databases.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static base_dotnet.Common.Enum;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class UserTypeFilterAttribute : Attribute, IAuthorizationFilter
{
    public UserTypeFilterAttribute(params UserRole[] args)
    {
        Args = args;
    }

    public UserRole[] Args { get; }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.Items["User"] as User;

        try
        {
           bool isCheck = false;
           for (int i = 0; i < Args.Length; i++)
           {
               if (user?.UserTypeId == Convert.ToUInt32(Args[i])) { isCheck = true; break; }
           }
           if (Args.Length == 0)
           {
               isCheck = true;
           }
           if (!isCheck)
           {
               context.Result = new JsonResult(new { message = "Permission denied!" }) { StatusCode = StatusCodes.Status403Forbidden };
           }
        }
        catch (Exception err)
        {
           Console.WriteLine(err.ToString());
        }
    }
}
