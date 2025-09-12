namespace CRM.Application.Dtos;

public class StudentSearchGetDto
{
	public string StudentName { get; set; } = string.Empty;
	public string CourseTitle { get; set; } = string.Empty;
	public double PaymentSum { get; set; }
}

