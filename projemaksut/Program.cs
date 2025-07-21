

using Application.Service;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Mapper;
using Infrastructure.Middlewares;
using Infrastructure.Validators;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());


builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
builder.Services.AddTransient<GlobalExceptionHandler>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection")),
         mysqlOptions => mysqlOptions.MigrationsAssembly("projemaksut")
    ));

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();


app.UseMiddleware<GlobalExceptionHandler>();
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
