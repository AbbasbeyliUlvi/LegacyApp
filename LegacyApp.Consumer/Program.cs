using LegacyApp.DbConnection;
using LegacyApp.Users;
using LegacyApp.Users.UserCredit;

namespace LegacyApp.Consumer;

class Program
{
    static void Main(string[] args)
    {
        AddUser(args);
    }

    public static void AddUser(string[] args)
    {
        // DO NOT CHANGE THIS FILE AT ALL

        IUserCreditService userCreditService = UserCreditServiceFactory.CreateUserCreditService();
        string? connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;
        DbConnectionFactory dbConnectionFactory = new DbConnectionFactory(connectionString);
        ClientRepository clientRepository = new ClientRepository(dbConnectionFactory);
        UserRepository userRepository = new UserRepository(dbConnectionFactory);

        var userService = new UserService(userRepository, clientRepository, userCreditService);
        var addResult = userService.AddUser("John", "Doe", "John.doe@gmail.com", new DateTime(1993, 1, 1), 4);
        Console.WriteLine("Adding John Doe was " + (addResult ? "successful" : "unsuccessful"));
    }
}