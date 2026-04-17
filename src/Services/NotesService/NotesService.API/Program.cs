using NotesService.Application.Features.Notes.Commands.CreateNote;
using NotesService.Infrastructure.Configurations;
using NotesService.Infrastructure.Persistence;
using NotesService.Infrastructure.Repositories;
using NotesService.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ------------------ CONTROLLERS ------------------
builder.Services.AddControllers();


// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------ MEDIATR ------------------
// Registers all handlers from Application layer
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateNoteHandler).Assembly));

// ------------------ MONGODB CONFIG ------------------

// Bind MongoSettings from appsettings.json
var mongoSettings = builder.Configuration
    .GetSection("MongoSettings")
    .Get<MongoSettings>()
    ?? throw new Exception("MongoSettings not configured properly");


// Register as Singleton (one instance for whole app)
builder.Services.AddSingleton(mongoSettings);

// Register DbContext (Mongo wrapper)
builder.Services.AddSingleton<NotesDbContext>();

// Register Repository (Dependency Injection)
builder.Services.AddScoped<INoteRepository, NoteRepository>();



// ------------------ BUILD APP ------------------
var app = builder.Build();

// Enable Swagger regardless of environment so the UI isn't 404 when ASPNETCORE_ENVIRONMENT isn't Development.
app.UseSwagger();       // Generates JSON
app.UseSwaggerUI();     // UI interface

app.UseHttpsRedirection();

app.UseAuthorization();

// Redirect root to Swagger UI so navigating to https://localhost:7247/ doesn't return 404.
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapControllers();

app.Run();