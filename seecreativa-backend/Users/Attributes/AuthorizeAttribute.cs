using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using seecreativa_backend.Users.Repositories;

namespace seecreativa_backend.Users.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly bool _admin;

        public AuthorizeAttribute(bool admin = false)
        {
            _admin = admin;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var serviceProvider = context.HttpContext.RequestServices;
            if (serviceProvider == null)
            {
                throw new Exception("Service provider is null.");
            }

            var authRepository = serviceProvider.GetService<AuthRepository>();
            if (authRepository == null)
            {
                throw new Exception("AuthRepository not found in service provider.");
            }

            var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader != null)
            {
                var token = authHeader.Replace("Bearer ", "");

                if (!token.IsNullOrEmpty())
                {
                    var user = authRepository!.Logged(token);
                    if (user != null)
                    {
                        if (user.IsAdmin == true || (_admin == false && user.IsAdmin == false))
                        {
                            return;
                        }
                    }
                    else
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }
                }
            }
            context.Result = new UnauthorizedResult();
        }
    }
}
