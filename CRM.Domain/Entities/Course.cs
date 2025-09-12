namespace CRM.Domain.Entities;

public class Course
{
	public int Id{get; set;}
	public string Title{ get; set;} = string.Empty;
	public int Duration{get; set;}
	public double Price{get; set;}
}

