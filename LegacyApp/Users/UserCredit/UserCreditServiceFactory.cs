using System.ServiceModel;
using System.ServiceModel.Channels;

namespace LegacyApp.Users.UserCredit;

public class UserCreditServiceFactory
{
    public static IUserCreditService CreateUserCreditService()
    {
        return new UserCreditServiceClient();
    }

    public static IUserCreditService CreateUserCreditService(string endpointConfigurationName)
    {
        if (string.IsNullOrEmpty(endpointConfigurationName))
        {
            throw new ArgumentException("Endpoint configuration name cannot be null or empty", nameof(endpointConfigurationName));
        }

        return new UserCreditServiceClient(endpointConfigurationName);
    }

    public static IUserCreditService CreateUserCreditService(Binding binding, EndpointAddress remoteAddress)
    {
        if (binding == null)
        {
            throw new ArgumentNullException(nameof(binding), "Binding cannot be null.");
        }

        if (remoteAddress == null)
        {
            throw new ArgumentNullException(nameof(remoteAddress), "Remote address cannot be null.");
        }

        return new UserCreditServiceClient(binding, remoteAddress);
    }
}
