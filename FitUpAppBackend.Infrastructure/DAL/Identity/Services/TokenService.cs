using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FitUpAppBackend.Core.Common.Services;
using FitUpAppBackend.Core.Identity.Configuration;
using FitUpAppBackend.Core.Identity.DTO;
using FitUpAppBackend.Core.Identity.Services;
using Microsoft.IdentityModel.Tokens;

namespace FitUpAppBackend.Infrastructure.DAL.Identity.Services;

public class TokenService : ITokenService
{
    private readonly IDateService _dateService;
    private readonly AuthConfig _authConfig;

    public TokenService(IDateService dateService, AuthConfig authConfig)
    {
        _dateService = dateService;
        _authConfig = authConfig;
    }
    
    public async Task<JsonWebToken> GenerateJwtAsync(Guid userId, string email, ICollection<string> roles, ICollection<Claim> claims, CancellationToken cancellationToken)
    {
        var now = _dateService.CurrentDate();
        var expires = now.Add(_authConfig.Expires);

        var issuer = _authConfig.JwtIssuer;
        
        var jwtClaims = new List<Claim>();
        
        if(roles.Count > 0)
            foreach (var role in roles)
                jwtClaims.Add(new Claim("role", role));
        
        if(claims.Count > 0)
            jwtClaims.AddRange(claims);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.JwtKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(issuer, issuer, jwtClaims, now, expires, signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JsonWebToken
        {
            AccessToken = token,
            Expires = new DateTimeOffset(expires).ToUnixTimeSeconds(),
            UserId = userId,
            Email = email,
            Roles = roles,
            Claims = claims.ToDictionary(claim => claim.Type, claim => claim.Value)
        };
    }
}