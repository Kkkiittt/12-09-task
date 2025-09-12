using CRM.Application.Dtos;
using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Services;

public interface IAttendanceService : IService<Attendance>
{
	public Task <IEnumerable<StudentMissingGetDto>> GetMissingAsync(DateOnly date);
}
