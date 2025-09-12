using CRM.Application.Dtos;
using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Services;

public interface ICourseService:IService<Course>
{
	public Task<IEnumerable<CourseOverviewGetDto>> GetStatsAsync();
}
