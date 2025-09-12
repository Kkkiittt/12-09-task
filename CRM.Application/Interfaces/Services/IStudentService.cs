using CRM.Application.Dtos;
using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Services;

public interface IStudentService : IService<Student>
{
	public Task<IEnumerable<StudentSearchGetDto>> SearchAsync(string name, int courseId, double paymentSum);
	public Task<IEnumerable<StudentCourseGetDto>> GetStudentsDetailedAsync();
}
