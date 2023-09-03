using JwtAspNet.Model;
using JwtAspNet.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();  

var app = builder.Build();


app.MapGet("/", (TokenService service) =>
{
    var user = new User(
    
        Id: 1,
        Name: "John Doe",
        Image: "https://avatars.githubusercontent.com/u/19608973?v=4",
        Email: "useremail@gmail.com",
        Password: "123456",
        Roles: new string[] { "admin", "user"}
    );

    return service.Create(user);
});

app.Run();
