using System;  
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LegacyApp.Users;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public UserRepository(IDbConnectionFactory dbConnectionFactory)
    {
        this._dbConnectionFactory = dbConnectionFactory;
    }
    public void AddUser(User user)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

        using (var connection = _dbConnectionFactory.CreateConnection())
        using (var command = new SqlCommand("uspAddUser", connection) { CommandType = CommandType.StoredProcedure })
        {
            AddSqlParameter(command, "@Firstname", SqlDbType.VarChar, 50, user.Firstname);
            AddSqlParameter(command, "@Surname", SqlDbType.VarChar, 50, user.Surname);
            AddSqlParameter(command, "@DateOfBirth", SqlDbType.DateTime, user.DateOfBirth);
            AddSqlParameter(command, "@EmailAddress", SqlDbType.VarChar, 50, user.EmailAddress);
            AddSqlParameter(command, "@HasCreditLimit", SqlDbType.Bit, user.HasCreditLimit);
            AddSqlParameter(command, "@CreditLimit", SqlDbType.Int, user.CreditLimit);
            AddSqlParameter(command, "@ClientId", SqlDbType.Int, user.Client.Id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    private static void AddSqlParameter(SqlCommand command, string parameterName, SqlDbType dbType, object value)
    {
        command.Parameters.Add(new SqlParameter(parameterName, dbType) { Value = value ?? DBNull.Value });
    }

    private static void AddSqlParameter(SqlCommand command, string parameterName, SqlDbType dbType, int size, object value)
    {
        command.Parameters.Add(new SqlParameter(parameterName, dbType, size) { Value = value ?? DBNull.Value });
    }
}