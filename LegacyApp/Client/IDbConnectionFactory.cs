using System.Data.SqlClient;

namespace LegacyApp;

public interface IDbConnectionFactory
{
    SqlConnection CreateConnection();
}
