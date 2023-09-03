using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JwtAspNet.Services;


public class TokenService
{
    public string Create()
    {
        var handler = new JwtSecurityTokenHandler();

        var credetials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateString)),
            SecurityAlgorithms.HmacSha256
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {           
            SigningCredentials = credetials,
            Expires = DateTime.UtcNow.AddHours(2)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
    
}