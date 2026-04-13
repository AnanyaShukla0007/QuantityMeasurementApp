using AuthService.Business.Interface;
using AuthService.Business.Services;
using AuthService.Repository.Data;
using AuthService.Repository.Interface;
using AuthService.Repository.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5001");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthServiceImpl>();
builder.Services.AddScoped<JwtServices>();
builder.Services.AddScoped<PasswordServices>();
builder.Services.AddScoped<EncryptionService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.MapControllers();

app.Run();