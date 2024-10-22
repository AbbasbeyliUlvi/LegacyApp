using LegacyApp.Users;
using LegacyApp.Users.UserCredit;

namespace LegacyApp;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IUserCreditService _userCreditService;

    public UserService(IUserRepository userRepository, IClientRepository clientRepository, IUserCreditService userCreditService)
    {
        this._userRepository = userRepository;
        _clientRepository = clientRepository;
        _userCreditService = userCreditService;
    }

    public bool AddUser(string firstname, string surname, string email, DateTime dateOfBirth, int clientId)
    {
        return AddUser(new User
        {
            Firstname = firstname,
            Surname = surname,
            EmailAddress = email,
            DateOfBirth = dateOfBirth
        },
        clientId);
    }

    public bool AddUser(User user, int clientId)
    {
        if (!IsValidUserInput(user))
        {
            return false;
        }

        var client = _clientRepository.GetById(clientId);

        if (!_userCreditService.AssignCreditLimit(user, client))
        {
            return false;
        }

        _userRepository.AddUser(user);
        return true;
    }

    private bool IsValidUserInput(User? user)
    {
        var userValidator = new UserValidator();
        var isValid = user != null && userValidator.Validate(user).IsValid;

        return isValid;
    }
}
