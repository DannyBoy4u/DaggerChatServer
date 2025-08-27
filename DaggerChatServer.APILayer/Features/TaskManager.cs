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

            // POST /work-tasks
            app.MapPost("/work-tasks",
                async ([FromBody] GetWorkItemFullDTO dto,
                       [FromServices] ITaskingQueries taskingQueries, // or your service interface that has InsertWorkTask
                       HttpContext ctx) =>
                {
                    if (dto is null) return Results.BadRequest();

                    var existenceCheck = await taskingQueries.TaskExistenceCheckByUrl(dto.UrlLink);

                    if (existenceCheck == true)
                    {
                        return Results.Accepted();
                    }

                    // Ensure an Id exists for persistence
                    if (dto.Id == Guid.Empty)
                        dto.Id = Guid.NewGuid();

                    // (Optional) minimal validation example
                    if (string.IsNullOrWhiteSpace(dto.Title))
                        return Results.ValidationProblem(new Dictionary<string, string[]>
                        {
                            ["Title"] = new[] { "Title is required." }
                        });

                    await taskingQueries.InsertWorkTask(dto);

                    // Return 201 with a Location header
                    var location = $"/work-tasks/{dto.Id}";
                    return Results.Created(location, dto);
                })
                .Accepts<GetWorkItemFullDTO>("application/json")
                .Produces<GetWorkItemFullDTO>(StatusCodes.Status201Created)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .WithName("CreateWorkTask")
                .WithTags("WorkTasks")
                .IncludeInOpenApi();

        }
    }
}

