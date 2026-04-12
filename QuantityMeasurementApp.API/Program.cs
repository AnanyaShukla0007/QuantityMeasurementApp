// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;
// using Microsoft.OpenApi.Models;
// using System.Text;
// using System.Text.Json;
// using System.Text.Json.Serialization;
// using QuantityMeasurementApp.Repository.Data;
// using QuantityMeasurementApp.Business.Interface;
// using QuantityMeasurementApp.Business.Services;
// using QuantityMeasurementApp.Repository.Interface;
// using QuantityMeasurementApp.Repository.Services;
// using QuantityMeasurementApp.API.Services;
// using QuantityMeasurementApp.API.Middleware;

// var builder = WebApplication.CreateBuilder(args);

// // ── CORS ─────────────────────────────────────────────
// var frontendUrl = builder.Configuration["FrontendUrl"] ?? "http://localhost:4200";
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("Angular", policy =>
//         policy.WithOrigins(
//             "http://localhost:4200",
//             "https://quantitymeasurementapp-lhbs.onrender.com"
//         )
//         .AllowAnyHeader()
//         .AllowAnyMethod()
//         .AllowCredentials());
// });

// // ── Controllers ──────────────────────────────────────
// builder.Services.AddControllers()
//     .AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//         options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
//     });

// builder.Services.AddEndpointsApiExplorer();

// // ── Swagger ──────────────────────────────────────────
// builder.Services.AddSwaggerGen(c =>
// {
//     c.EnableAnnotations();
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quantity Measurements", Version = "v1" });
//     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//     {
//         Name = "Authorization",
//         Type = SecuritySchemeType.Http,
//         Scheme = "bearer",
//         BearerFormat = "JWT",
//         In = ParameterLocation.Header
//     });
//     c.AddSecurityRequirement(new OpenApiSecurityRequirement
//     {
//         {
//             new OpenApiSecurityScheme
//             {
//                 Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
//             },
//             new string[] {}
//         }
//     });
// });

// // ── Database (PostgreSQL) ─────────────────────────────
// builder.Services.AddDbContext<QuantityDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// // ── Redis (optional - falls back to in-memory) ────────
// var redisConn = builder.Configuration["Redis:ConnectionString"];
// if (!string.IsNullOrEmpty(redisConn))
// {
//     builder.Services.AddStackExchangeRedisCache(options =>
//         options.Configuration = redisConn);
// }
// else
// {
//     builder.Services.AddDistributedMemoryCache();
// }

// // ── Services ─────────────────────────────────────────
// builder.Services.AddScoped<UserRepository>();
// builder.Services.AddScoped<PasswordService>();
// builder.Services.AddScoped<RedisCacheService>();
// builder.Services.AddScoped<EncryptionService>();
// builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
// builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementEfRepository>();

// // ── JWT ──────────────────────────────────────────────
// var jwtKey   = builder.Configuration["Jwt:Key"];
// var issuer   = builder.Configuration["Jwt:Issuer"];
// var audience = builder.Configuration["Jwt:Audience"];

// if (string.IsNullOrEmpty(jwtKey) || Encoding.UTF8.GetBytes(jwtKey).Length < 32)
//     throw new Exception("JWT key must be at least 32 characters.");

// builder.Services.AddSingleton(new JwtService(jwtKey, issuer!, audience!));

// // ── Authentication (JWT only) ────────────────────────
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// .AddJwtBearer(options =>
// {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = issuer,
//         ValidAudience = audience,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
//     };
// });

// // ─────────────────────────────────────────────────────
// var app = builder.Build();

// // ── Auto-migrate on startup ───────────────────────────
// // using (var scope = app.Services.CreateScope())
// // {
// //     var db = scope.ServiceProvider.GetRequiredService<QuantityDbContext>();
// //     db.Database.Migrate();
// // }
// using (var scope = app.Services.CreateScope())
// {
//     try
//     {
//         var db = scope.ServiceProvider.GetRequiredService<QuantityDbContext>();
//         db.Database.Migrate();
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"Database migration skipped: {ex.Message}");
//     }
// }

// app.UseMiddleware<GlobalExceptionMiddleware>();

// app.UseSwagger();
// app.UseSwaggerUI(c =>
// {
//     c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
//     c.RoutePrefix = string.Empty;
// });

// app.UseCors("Angular");
// app.UseAuthentication();
// app.UseAuthorization();
// app.MapControllers();
// app.Run();
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using QuantityMeasurementApp.Repository.Data;
using QuantityMeasurementApp.Business.Interface;
using QuantityMeasurementApp.Business.Services;
using QuantityMeasurementApp.Repository.Interface;
using QuantityMeasurementApp.Repository.Services;
using QuantityMeasurementApp.API.Services;
using QuantityMeasurementApp.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ── CORS ─────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
                origin == "http://localhost:4200" ||
                origin.Contains("vercel.app"))
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// ── Controllers ──────────────────────────────────────
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();

// ── Swagger ──────────────────────────────────────────
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quantity Measurements", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

// ── Database ─────────────────────────────────────────
builder.Services.AddDbContext<QuantityDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// ── Cache ────────────────────────────────────────────
var redisConn = builder.Configuration["Redis:ConnectionString"];

if (!string.IsNullOrEmpty(redisConn))
{
    builder.Services.AddStackExchangeRedisCache(options =>
        options.Configuration = redisConn);
}
else
{
    builder.Services.AddDistributedMemoryCache();
}

// ── Services ─────────────────────────────────────────
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<RedisCacheService>();
builder.Services.AddScoped<EncryptionService>();
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();
builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementEfRepository>();

// ── JWT ──────────────────────────────────────────────
var jwtKey = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];

if (string.IsNullOrEmpty(jwtKey) || Encoding.UTF8.GetBytes(jwtKey).Length < 32)
    throw new Exception("JWT key must be at least 32 characters.");

builder.Services.AddSingleton(new JwtService(jwtKey, issuer!, audience!));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey))
    };
});

// ─────────────────────────────────────────────────────
var app = builder.Build();

// ── Migration ────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<QuantityDbContext>();

        db.Database.ExecuteSqlRaw(@"DROP TABLE IF EXISTS ""Measurements"";");
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty;
});

app.UseCors("Angular");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();