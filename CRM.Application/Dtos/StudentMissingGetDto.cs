namespace CRM.Application.Dtos;

public class StudentMissingGetDto
{
	public string StudentName { get; set; } = string.Empty;
	public string GroupName { get; set; } = string.Empty;
	public string CourseTitle { get; set; } = string.Empty;
	public DateTime LessonAt { get; set; }
}

