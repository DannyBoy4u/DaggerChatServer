using APILayer.Queries;
using Carter;
using DatabaseLayer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Allow WPF client on localhost to connect
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(p =>
        p.SetIsOriginAllowed(_ => true)
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials());
});

builder.Services.AddSignalR();

builder.Services.AddScoped<ITaskingQueries, TaskingQueries>();

builder.Services.AddCarter();

// For Minimal APIs & Carter discovery
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


DotNetEnv.Env.Load();

string connectionString = Environment.GetEnvironmentVariable("DefaultConnectionString");
// Saving the command below for future use:
//dotnet ef database update -p ".\DaggerChatServer.DatabaseLayer\DatabaseLayer.csproj" -s ".\DaggerChatServer.APILayer\APILayer.csproj"
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

// Apply pending migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    db.Database.Migrate();   // creates DB if needed, applies all pending migrations
}

// Enable Swagger UI (dev-only typical pattern)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // /swagger
}

app.UseCors();

// Simple root check
app.MapGet("/", () => "ChatServer OK");

// SignalR hub
app.MapHub<ChatHub>("/chatHub");
app.MapCarter();
app.Run("http://localhost:5080"); // use HTTP to avoid dev cert hassles

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        // Broadcast to everyone (including sender)
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
