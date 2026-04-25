using System.Text; // For encoding JWT secret into bytes
using MediatR; // Mediator pattern library
// Enables mediator pattern → decouples controllers from business logic
using Microsoft.AspNetCore.Authentication.JwtBearer; // JWT authentication middleware
using Microsoft.IdentityModel.Tokens; // Token validation classes
using Microsoft.OpenApi.Models; // Swagger models
using UserService.Application.Features.Auth.Commands.Login; // Login handler assembly reference
using UserService.Application.Features.Auth.Commands.Register; // Register handler assembly reference
using UserService.Application.Interfaces; // Interfaces like IUserRepository
using UserService.Infrastructure.Persistence; // DB connection factory
using UserService.Infrastructure.Repositories; // Repository implementation
using SharedLibrary.Infrastructure.GlobalException; // Custom exception middleware


// Entry point of ASP.NET Core app
// Creates builder object that:
// - Loads configuration (appsettings.json, env vars)
// - Sets up dependency injection container
// - Prepares middleware pipeline
var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddControllers(); // Enable controllers and attribute based routing
builder.Services.AddEndpointsApiExplorer(); // Required for Swagger auto api routing

// Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UserService API",
        Version = "v1"
    });

    // Add JWT support in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization", // Header name
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token here"
    });

    // Apply JWT globally
    // Enforces security globally for Swagger endpoints
    // So all endpoints will require JWT unless marked [AllowAnonymous]
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Register MediatR handlers
// Registers MediatR in DI container
// Assembly scanning:Finds all handlers -- Automatically wires them
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(LoginHandler).Assembly));

// Get DB connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("DefaultConnection is missing.");

// Register DB connection factory (singleton)
builder.Services.AddSingleton(new DbConnectionFactory(connectionString));

// Register repository (per request)
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Get JWT secret
var secret = builder.Configuration["JwtSettings:Secret"]
    ?? throw new InvalidOperationException("JwtSettings:Secret is missing.");

// Configure JWT authentication
// Sets default scheme to JWT Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Ensures token issuer is valid
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            //expected issuer value
            
            //expected audience
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtSettings:Audience"],

            // token expiry check
            ValidateLifetime = true,
            // token signature check
            ValidateIssuerSigningKey = true,

            // Converts secret string → byte[] → security key
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secret)),

            ClockSkew = TimeSpan.Zero // No tolerance for expiry
        };
    });

// Enable authorization
builder.Services.AddAuthorization();

// Builds the application pipeline
// DI container is now locked
var app = builder.Build(); 

// Global exception handling middleware
app.UseGlobalExceptionMiddleware();

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Enforce HTTPS
app.UseHttpsRedirection();

// Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controller routes
app.MapControllers();

// Start application
app.Run();