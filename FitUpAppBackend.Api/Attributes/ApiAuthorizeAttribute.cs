using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace FitUpAppBackend.Api.Attributes;

public sealed class ApiAuthorizeAttribute : AuthorizeAttribute
{
    public ApiAuthorizeAttribute() : base()
    {
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
    }
}