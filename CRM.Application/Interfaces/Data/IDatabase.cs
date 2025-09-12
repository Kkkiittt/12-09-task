using System.Data.Common;

namespace CRM.Application.Interfaces.Data;

public interface IDatabase
{
	public DbConnection CreateConnection();
}