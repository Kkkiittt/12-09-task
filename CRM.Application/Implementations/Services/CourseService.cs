
using CRM.Application.Dtos;
using CRM.Application.Interfaces.Data;
using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;

using Dapper;

namespace CRM.Application.Implementations.Services;

public class CourseService : ICourseService
{
	private readonly IDatabase _db;

	public CourseService(IDatabase db)
	{
		_db = db;
	}

	public async Task<bool> CreateAsync(Course entity)
	{
		string query = "INSERT INTO courses(title, duration, price) values(@title, @duration, @price)";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.ExecuteAsync(query, entity) > 0;
		}
	}

	public async Task<IEnumerable<Course>> GetAllAsync()
	{
		string query = "SELECT * FROM courses";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryAsync<Course>(query);
		}
	}

	public async Task<Course> GetAsync(int id)
	{
		string query = "SELECT * FROM courses WHERE id = @id";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryFirstOrDefaultAsync<Course>(query, new { id }) ?? throw new Exception("not found");
		}
	}

	public async Task<IEnumerable<CourseOverviewGetDto>> GetStatsAsync()
	{
		string query = """
			select 
				c.Title as courseTitle,
				count(distinct sg.studentId) as studentCount,
				sum(p.amount) as paymentSum
			from courses as c
			join groups as g on c.id = g.courseId
			join studentGroups as sg on g.id = sg.groupId
			join payments as p on sg.id = p.studentGroupId
			group by c.id
			""";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryAsync<CourseOverviewGetDto>(query);
		}
	}
}

