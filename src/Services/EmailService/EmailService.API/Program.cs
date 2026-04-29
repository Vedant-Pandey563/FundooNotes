using EmailService.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RabbitMQ consumer for the registration-email flow.
builder.Services.AddHostedService<RabbitMQConsumer>();

var app = builder.Build();

// Required for Dapr pub/sub topic subscriptions.
app.UseCloudEvents();
app.MapSubscribeHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();