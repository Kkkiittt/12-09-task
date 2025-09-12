using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Services;

public interface IStudentGroupService
{
	Task<bool> AddAsync(int studentId, int groupId);
	Task<IEnumerable<StudentGroup>> GetAllAsync();
}
