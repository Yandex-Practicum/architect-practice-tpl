using ParkingApp.Services.Employees;
using ParkingApp.Services.Managers;

var builder = WebApplication.CreateBuilder(args);

for (int i = 0; i < args.Count(); i++)
{
    try
    {
        if (args[i] == "-a")
        {
            Environment.SetEnvironmentVariable("RUN_ADDRESS", args[i + 1]);
        }
        if (args[i] == "-s")
        {
            Environment.SetEnvironmentVariable("SECRET_CODE", args[i + 1]);
        }
    }
    catch (Exception ex)
    {
        throw new Exception($"Ошибка применения аргументов командной строки: {ex.Message}");
    }
    
}
;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IManagersService, ManagersService>();
builder.Services.AddTransient<IEmployeesService, EmployeesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
