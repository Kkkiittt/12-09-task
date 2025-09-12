
using CRM.Application.Dtos;
using CRM.Application.Interfaces.Data;
using CRM.Application.Interfaces.Services;
using CRM.Domain.Entities;

using Dapper;

namespace CRM.Application.Implementations.Services;

public class PaymentService : IPaymentService
{
	private readonly IDatabase _db;
	public PaymentService(IDatabase db)
	{
		_db = db;
	}

	public async Task<bool> CreateAsync(Payment entity)
	{
		string query = "insert into payments(studentGroupId, amount, paidAt) values(@studentGroupId, @amount, current_timestamp)";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.ExecuteAsync(query, entity) > 0;
		}
	}

	public async Task<IEnumerable<Payment>> GetAllAsync()
	{
		string query = "select * from payments";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryAsync<Payment>(query);
		}
	}

	public async Task<Payment> GetAsync(int id)
	{
		string query = "select * from payments where id = @id";
		using(var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryFirstOrDefaultAsync<Payment>(query, new { id }) ?? throw new Exception("not found");
		}
	}

	public async Task<IEnumerable<PaymentCourseGetDto>> GetDetailedPaymentsAsync()
	{
		string query = """
			select
				s.name as studentName,
				g.name as groupName,
				c.title as courseTitle,
				p.amount,
				p.paidAt
			from payments as p
			join studentGroups as sg on p.studentGroupId = sg.id
			join groups as g on sg.groupId = g.id
			join students as s on sg.studentId = s.id
			join courses as c on g.courseId = c.id
			""";
		using (var conn = _db.CreateConnection())
		{
			conn.Open();
			return await conn.QueryAsync<PaymentCourseGetDto>(query);
		}
	}
}

