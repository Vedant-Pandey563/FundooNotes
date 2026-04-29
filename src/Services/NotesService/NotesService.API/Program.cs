using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NotesService.Application.Features.Notes.Commands.CreateNote;
using NotesService.Application.Interfaces;
//using NotesService.Domain.Configurations;
using NotesService.Infrastructure.Configurations;
using NotesService.Infrastructure.Persistence;
using NotesService.Infrastructure.Repositories;
using NotesService.Infrastructure.Cache;
using System.Text;
using SharedLibrary.Infrastructure.GlobalException;
using Dapr.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NotesService API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
    });

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

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateNoteHandler).Assembly));

// Mongo
var mongoSettings = builder.Configuration
    .GetSection("MongoSettings")
    .Get<MongoSettings>()
    ?? throw new InvalidOperationException("MongoSettings is missing.");

builder.Services.AddSingleton(mongoSettings);
builder.Services.AddSingleton<NotesDbContext>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();

// JWT
var jwtSecret = builder.Configuration["JwtSettings:Secret"]
    ?? throw new InvalidOperationException("JwtSettings:Secret is missing.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtSettings:Audience"],

            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ClockSkew = TimeSpan.Zero
        };
    });

// Dapr state store cache.
// IMPORTANT: this requires the service to run with a Dapr sidecar.
builder.Services.AddDaprClient();
builder.Services.AddScoped<ICacheService, DaprCacheService>();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseGlobalExceptionMiddleware();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapControllers();
app.Run();