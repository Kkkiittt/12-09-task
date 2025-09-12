

using CRM.Application.Dtos;
using CRM.Application.Interfaces.Data;
using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;

using Dapper;

namespace CRM.Application.Implementations.Services;

public class AttendanceService : IAttendanceService
{
	private readonly IDatabase _db;

	public AttendanceService(IDatabase db)
	{
		_db = db;
	}

	public async Task<bool> CreateAsync(Attendance entity)
	{
		string query = "INSERT INTO attendances(studentGroupId, lessonAt, present) values(@studentGroupId, @lessonAt, @present)";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			var paramEntity = new { entity.StudentGroupId, LessonAt = new DateTime(entity.LessonAt.Year, entity.LessonAt.Month, entity.LessonAt.Day), entity.Present };
			return await conn.ExecuteAsync(query, paramEntity) > 0;
		}
	}

	public async Task<IEnumerable<Attendance>> GetAllAsync()
	{
		string query = "SELECT * FROM attendances";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryAsync<Attendance>(query);
		}
	}

	public async Task<Attendance> GetAsync(int id)
	{
		string query = "SELECT * FROM attendances WHERE id = @id";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryFirstOrDefaultAsync<Attendance>(query, new { id }) ?? throw new Exception("not found");
		}
	}

	public async Task<IEnumerable<StudentMissingGetDto>> GetMissingAsync(DateOnly date)
	{
		string query = """
			SELECT 
				s.Name as studentName,
				g.Name as groupName,
				c.Title as courseTitle,
				a.lessonAt
			FROM attendances as a
			JOIN studentGroups as sg on a.studentGroupId = sg.id
			JOIN students as s on sg.studentId = s.id
			JOIN groups as g on sg.groupId = g.id
			JOIN courses as c on g.courseId = c.id
			WHERE Present=false AND a.lessonAt = @date;
			""";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryAsync<StudentMissingGetDto>(query, new { date=new DateTime(date.Year, date.Month, date.Day) });
		}
	}
}

