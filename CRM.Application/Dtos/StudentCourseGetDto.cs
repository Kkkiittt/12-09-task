namespace CRM.Application.Dtos;

public class StudentCourseGetDto
{
	public string StudentName { get; set; } = string.Empty;
	public string GroupName { get; set; } = string.Empty;
	public DateTime GroupStartedAt { get; set; }
	public string CourseTitle { get; set; } = string.Empty;
	public int CourseDuration { get; set; }
}

