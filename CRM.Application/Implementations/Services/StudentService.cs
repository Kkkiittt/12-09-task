
using CRM.Application.Dtos;
using CRM.Application.Interfaces.Data;
using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;

using Dapper;

namespace CRM.Application.Implementations.Services;

public class StudentService : IStudentService
{
	private readonly IDatabase _db;
	public StudentService(IDatabase db)
	{
		_db = db;
	}

	public async Task<bool> CreateAsync(Student entity)
	{
		string query = "INSERT INTO students(name, phone) values(@name, @phone)";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.ExecuteAsync(query, entity) > 0;
		}
	}

	public async Task<IEnumerable<Student>> GetAllAsync()
	{
		string query = "SELECT * FROM students";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryAsync<Student>(query);
		}
	}

	public async Task<Student> GetAsync(int id)
	{
		string query = "SELECT * FROM students WHERE id = @id";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryFirstOrDefaultAsync<Student>(query, new { id }) ?? throw new Exception("not found");
		}
	}

	public async Task<IEnumerable<StudentCourseGetDto>> GetStudentsDetailedAsync()
	{
		string query = """
			select 
				s.name as StudentName,
				g.name as GroupName,
				DATE(g.startedAt) as GroupStartedAt,
				c.title as CourseTitle,
				c.duration as CourseDuration
			from students as s
			join studentGroups as sg on s.id = sg.studentId
			join groups as g on sg.groupId = g.id
			join courses as c on g.courseId = c.id
			""";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryAsync<StudentCourseGetDto>(query);
		}
	}

	public async Task<IEnumerable<StudentSearchGetDto>> SearchAsync(string name, int courseId, double paymentSum)
	{
		Console.WriteLine("|"+name+"|");
		string query = $"""
			select
				s.name as studentname,
				sum(p.amount) as paymentSum,
				c.title as coursetitle
			from students as s 
			join studentGroups as sg on s.id = sg.studentId
			join groups as g on sg.groupId = g.id
			join courses as c on g.courseId = c.id
			join payments as p on sg.id = p.studentGroupId
			where 
				s.name like '%{name}%' and
				c.id = @courseId
			group by s.name, c.title
			having sum(p.amount) >= @paymentSum
			""";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryAsync<StudentSearchGetDto>(query, new { name, courseId, paymentSum });
		}
	}
}

