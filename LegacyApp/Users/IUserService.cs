using LegacyApp.Users;

namespace LegacyApp
{
    public interface IUserService
    {
        bool AddUser(string firstname, string surname, string email, DateTime dateOfBirth, int clientId);
        bool AddUser(User user, int clientId);
    }
}