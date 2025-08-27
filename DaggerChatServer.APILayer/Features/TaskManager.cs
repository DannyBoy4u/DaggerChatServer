using Carter;

namespace DaggerChatServer.Features;

public class TaskManager
{
    public TaskManager()
    {
    }


    public class TaskModules : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            //app.MapGet("/tasks", )
        }
    }
}

