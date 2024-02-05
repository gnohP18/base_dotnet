using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using base_dotnet.Services;

namespace base_dotnet.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, ITokenService tokenService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
               AttachUserToContext(context, userService, tokenService, token);
            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IUserService userService, ITokenService tokenService, string token)
        {
            try
            {
               var userId = tokenService.ValidateToken(token);
               if (userId != null)
                   context.Items["User"] = userService.GetUserById(userId.Value);
            }
            catch (Exception)
            {
               throw new Exception("Token Invalid or Expire");
            }
        }
    }

}