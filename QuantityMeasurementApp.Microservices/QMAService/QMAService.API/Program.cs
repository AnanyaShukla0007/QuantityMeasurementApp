using Microsoft.EntityFrameworkCore;
using QMAService.Business.Interface;
using QMAService.Business.Services;
using QMAService.Repository.Data;
using QMAService.Repository.Interface;
using QMAService.Repository.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5002");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<QuantityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementRepository>();
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();

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