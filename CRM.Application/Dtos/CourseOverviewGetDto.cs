namespace CRM.Application.Dtos;

public class CourseOverviewGetDto
{
	public string CourseTitle {get; set;} = string.Empty;
	public int StudentsCount {get; set;} 
	public double PaymentSum {get; set;}
}

