
using CRM.Application.Interfaces.Data;
using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;

using Dapper;

namespace CRM.Application.Implementations.Services;

public class GroupService : IGroupService
{
	private readonly IDatabase _db;

	public GroupService(IDatabase db)
	{
		_db = db;
	}

	public async Task<bool> CreateAsync(Group entity)
	{
		string query = "INSERT into groups(courseId, name, startedAt) values (@courseId, @name, @startedAt)";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			var paramEntity = new { entity.CourseId, entity.Name, StartedAt = new DateTime(entity.StartedAt.Year, entity.StartedAt.Month, entity.StartedAt.Day) };
			return await conn.ExecuteAsync(query, paramEntity) > 0;
		}
	}

	public async Task<IEnumerable<Group>> GetAllAsync()
	{
		string query = "SELECT * FROM groups";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryAsync<Group>(query);
		}
	}

	public async Task<Group> GetAsync(int id)
	{
		string query = "SELECT * FROM groups WHERE id = @id";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryFirstOrDefaultAsync<Group>(query, new { id }) ?? throw new Exception("not found");
		}
	}
}

