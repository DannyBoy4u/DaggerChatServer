using APILayer.DTOs;
using APILayer.Queries;
using Carter;
using Carter.OpenApi;
using Mapster;
using Microsoft.AspNetCore.Mvc;

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
            app.MapGet("/work-tasks", async ([FromServices] ITaskingQueries taskingQueries, HttpResponse res) =>
            {
                var workItems = await taskingQueries.GetAllTasks();
                return workItems.Adapt<List<GetWorkItemFullDTO>>();
            }).Produces<List<GetWorkItemFullDTO>>(200).IncludeInOpenApi();

        }
    }
}

