using System;
using System.Data;
using System.Data.SqlClient;

namespace LegacyApp;

public class ClientRepository : IClientRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public ClientRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory), "DB Connection Factory cannot be null.");
    }

    public Client? GetById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Client ID must be greater than zero.", nameof(id));
        }

        using (var connection = _dbConnectionFactory.CreateConnection())
        {
            using (var command = CreateGetClientCommand(connection, id))
            {
                connection.Open();

                using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        return MapToClient(reader);
                    }
                }
            }
        }

        return null;
    }

    private SqlCommand CreateGetClientCommand(SqlConnection connection, int clientId)
    {
        var command = new SqlCommand
        {
            Connection = connection,
            CommandType = CommandType.StoredProcedure,
            CommandText = "uspGetClientById"
        };

        var parameter = new SqlParameter("@clientId", SqlDbType.Int) { Value = clientId };
        command.Parameters.Add(parameter);

        return command;
    }

    private Client MapToClient(SqlDataReader reader)
    {
        return new Client
        {
            Id = Convert.ToInt32(reader["ClientId"]),
            Name = reader["Name"].ToString(),
            ClientStatus = (ClientStatus)Enum.Parse(typeof(ClientStatus), reader["ClientStatus"].ToString())
        };
    }
}
