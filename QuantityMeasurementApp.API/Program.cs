using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Repository.Data;
//using QuantityMeasurementApp.API.Data;
using QuantityMeasurementApp.Business.Interface;
using QuantityMeasurementApp.Business.Services;
using QuantityMeasurementApp.Repository.Interface;
using QuantityMeasurementApp.Repository.Services;
using System.Text.Json.Serialization;
using QuantityMeasurementApp.API.Middleware;
using QuantityMeasurementApp.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters
        .Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<RedisCacheService>();
builder.Services.AddDbContext<QuantityDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
    options.InstanceName = "QuantityCache_";
});        

builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();

builder.Services.AddScoped<IQuantityMeasurementRepository,
                           QuantityMeasurementEfRepository>();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => Results.Redirect("/swagger"))
   .ExcludeFromDescription();
app.MapControllers();

app.Run();