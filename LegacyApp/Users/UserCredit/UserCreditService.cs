using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace LegacyApp.Users.UserCredit;

[ServiceContract(ConfigurationName = "LegacyApp.IUserCreditService")]
public interface IUserCreditService
{
    [OperationContract(Action = "http://totally-real-service.com/IUserCreditService/GetCreditLimit")]
    int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth);

    bool AssignCreditLimit(User user, Client client);
}

public interface IUserCreditServiceChannel : IUserCreditService, IClientChannel
{
}

[DebuggerStepThrough]
public partial class UserCreditServiceClient : ClientBase<IUserCreditService>, IUserCreditService
{
    public UserCreditServiceClient() : base() { }

    public UserCreditServiceClient(string endpointConfigurationName)
        : base(endpointConfigurationName) { }

    public UserCreditServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
        : base(endpointConfigurationName, remoteAddress) { }

    public UserCreditServiceClient(Binding binding, EndpointAddress remoteAddress)
        : base(binding, remoteAddress) { }

    public int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth)
    {
        return Channel.GetCreditLimit(firstname, surname, dateOfBirth);
    }
    public bool AssignCreditLimit(User user, Client client)
    {
        if (client.Name == ClientName.VeryImportantClient.ToString())
        {
            user.HasCreditLimit = false;
            return true;
        }

        user.HasCreditLimit = true;
        int creditLimit = GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);

        if (client.Name == ClientName.VeryImportantClient.ToString())
        {
            creditLimit *= 2;
        }

        user.CreditLimit = creditLimit;

        return CreditLimitIsMoreThanExpected(user);
    }

    private static bool CreditLimitIsMoreThanExpected(User user)
    {
        return user.CreditLimit >= 500;
    }
}
