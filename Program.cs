using Microsoft.AspNetCore.SignalR;

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

var app = builder.Build();

app.UseCors();

// Simple root check
app.MapGet("/", () => "ChatServer OK");

// SignalR hub
app.MapHub<ChatHub>("/chatHub");

app.Run("http://localhost:5080"); // use HTTP to avoid dev cert hassles

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        // Broadcast to everyone (including sender)
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
