using System.Diagnostics;

using CRM.Application.Implementations.Services;
using CRM.Application.Interfaces.Data;
using CRM.Application.Interfaces.Services;
using CRM.Infrastructure;

//for(int i = 0; i < 1; i++)
//{
//	Process process = new Process();
//	process.StartInfo.FileName = "calc1";
//	process.Start();
//}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IDatabase, Database>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IStudentGroupService, StudentGroupService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
	app.MapOpenApi("swagger/v1/swagger.json");
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
