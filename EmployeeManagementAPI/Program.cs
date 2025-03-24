using EmployeeManagement.DataAccess.Authentication;
using EmployeeManagement.DataAccess.DailyReportDetails;
using EmployeeManagement.DataAccess.Leave;
using EmployeeManagement.DataAccess.Staff;
using EmployeeManagement.Service.Authentication;
using EmployeeManagement.Service.DailyReportDetails;
using EmployeeManagement.Service.Leave;
using EmployeeManagement.Service.Staff;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Injecting service classes
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<IStaffService, StaffService>();
builder.Services.AddSingleton<IDailyReportService, DailyReportService>();
builder.Services.AddSingleton<ILeaveService, LeaveService>();
// Injecting data access classes
builder.Services.AddSingleton<IAuthenticationDataAccess, AuthenticationDataAccess>();
builder.Services.AddSingleton<IStaffDataAccess, StaffDataAccess>();
builder.Services.AddSingleton<IDailyReportDataAccess, DailyReportDataAccess>();
builder.Services.AddSingleton<ILeaveDataAccess, LeaveDataAccess>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins(
                        "https://developers.iitcglobal.net", "http://localhost:4200")
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
            );
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Correct middleware order
app.UseRouting(); // Add UseRouting before UseEndpoints

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
