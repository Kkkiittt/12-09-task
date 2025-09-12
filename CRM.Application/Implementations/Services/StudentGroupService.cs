
using CRM.Application.Interfaces.Data;
using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;

using Dapper;

namespace CRM.Application.Implementations.Services;

public class StudentGroupService : IStudentGroupService
{
	private readonly IDatabase _db;

	public StudentGroupService(IDatabase db)
	{
		_db = db;
	}

	public async Task<bool> AddAsync(int studentId, int groupId)
	{
		string query = "INSERT INTO studentGroups(studentId, groupId, joinedat) values(@studentId, @groupId, current_timestamp)";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.ExecuteAsync(query, new { studentId, groupId }) > 0;
		}
	}

	public Task<IEnumerable<StudentGroup>> GetAllAsync()
	{
		string query = "SELECT * FROM studentGroups";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return conn.QueryAsync<StudentGroup>(query);
		}
	}
}

