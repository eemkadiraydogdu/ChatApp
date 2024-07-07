using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace ChatApp.Services.Helpers;

public static class JwtToken
{
    public static string GenerateToken(TokenModel tokenModel)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, tokenModel.UserName),
            new Claim(ClaimTypes.Role, tokenModel.Role),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenModel.SigningKey));
        var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken (
            issuer : tokenModel.Issuer,
            audience : tokenModel.Audience,
            claims : claims,
            expires : DateTime.Now.AddHours(24),
            signingCredentials : credentials,
            notBefore: DateTime.Now
        ); 
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return token;

    }
}
