using System.Security.Claims;
using System.Text;
using JwtAspNet;
using JwtAspNet.Extensios;
using JwtAspNet.Model;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateString))
    };
});

builder.Services.AddAuthorization(x => 
{
    x.AddPolicy("admin", policy => policy.RequireRole("admin"));
    x.AddPolicy("development", policy => policy.RequireRole("development"));
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", (TokenService service) =>
{
    var user = new User(

        Id: 1,
        Name: "John Doe",
        Image: "https://avatars.githubusercontent.com/u/19608973?v=4",
        Email: "useremail@gmail.com",
        Password: "123456",
        Roles: new string[] { "admin", "user" }
    );

    return service.Create(user);
});
app.MapGet("/restrito", (TokenService service, ClaimsPrincipal user) =>
{
    return new 
    {
        Id = user.GetId(),
        Login = user.GetLogin(),
        Name = user.GetName(),
        Email = user.GetEmail(),
        Image = user.GetImage()
    };

}).RequireAuthorization("admin");

app.Run();
