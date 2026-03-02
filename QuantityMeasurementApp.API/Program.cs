using QuantityMeasurementApp.API.Extensions;
using QuantityMeasurementApp.API.Middleware;


var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Add Services to Container
// ----------------------------

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Custom Layers
builder.Services.AddQuantityMeasurementServices();

var app = builder.Build();

// ----------------------------
// Configure HTTP Pipeline
// ----------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapControllers();

app.Run();