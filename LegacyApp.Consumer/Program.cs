using LegacyApp.Users;
using LegacyApp.Users.UserCredit;

namespace LegacyApp.Consumer;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        services
            .AddSingleton<IUserCreditService, UserCreditServiceClient>()
            .AddSingleton<IClientRepository, ClientRepository>()
            .AddSingleton<IUserRepository, UserRepository>()
            .AddSingleton<IUserService, UserService>();

        var app = builder.Build();

        AddUser(app);
    }

    public static void AddUser(WebApplication app)
    {
        // DO NOT CHANGE THIS FILE AT ALL

        var userService = app.Services.GetService<IUserService>();
        var addResult = userService.AddUser("John", "Doe", "John.doe@gmail.com", new DateTime(1993, 1, 1), 4);

        Console.WriteLine("Adding John Doe was " + (addResult ? "successful" : "unsuccessful"));
    }
}