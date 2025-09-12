using System.Data.Common;

using CRM.Application.Interfaces.Data;

using Npgsql;

namespace CRM.Infrastructure;

public class Database : IDatabase
{
	private readonly string _conn;

	public Database(string conn = "host=localhost;port=5432;database=CRMDB;user id=postgres;password=somepass")
	{
		_conn = conn;
	}

	public DbConnection CreateConnection()
	{
		return new NpgsqlConnection(_conn);
	}
}

