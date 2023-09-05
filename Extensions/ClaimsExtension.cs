using System.Security.Claims;

namespace JwtAspNet.Extensios;

public static class ClaimsExtension
{
    public static int GetId(this ClaimsPrincipal user)
    {
        try
        {
            return int.Parse(user.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "0");
        }
        catch
        {
            return 0;
        }

    } 

    public static string GetLogin(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;
    }

    public static string GetName(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
    }

    public static string GetEmail(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;
    }

    public static string GetImage(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(x => x.Type == "image")?.Value ?? string.Empty;
    }
    
    
}