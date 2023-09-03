using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using JwtAspNet.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JwtAspNet.Services;


public class TokenService
{
    public string Create(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var credetials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateString)),
            SecurityAlgorithms.HmacSha256
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {           
            SigningCredentials = credetials,
            Expires = DateTime.UtcNow.AddHours(2),
            Subject = GenerateClaimsIdentity(user)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
    
    private static ClaimsIdentity GenerateClaimsIdentity(User user)
    {
        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim(type: "id", value: user.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(type: ClaimTypes.Name, value: user.Email));
        claimsIdentity.AddClaim(new Claim(type: ClaimTypes.Email, value: user.Email));
        claimsIdentity.AddClaim(new Claim(type: ClaimTypes.GivenName, value: user.Name));
        claimsIdentity.AddClaim(new Claim(type: "image", value: user.Image));

        foreach (var role in user.Roles)
        {
            claimsIdentity.AddClaim(new Claim(type: ClaimTypes.Role, value: role));
        }


        return claimsIdentity;
    }
}