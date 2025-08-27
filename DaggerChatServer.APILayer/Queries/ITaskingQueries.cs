using APILayer.DTOs;
using DatabaseLayer;
using DatabaseLayer.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace APILayer.Queries
{
    public interface ITaskingQueries
    {
        Task<List<WorkTask>> GetAllTasks();
        Task InsertWorkTask(GetWorkItemFullDTO workTask);
        Task<bool> TaskExistenceCheckByUrl(string url);
    }

    public class TaskingQueries : ITaskingQueries
    {
        public readonly DatabaseContext _databaseContext;
        public TaskingQueries(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<WorkTask>> GetAllTasks() => await _databaseContext.WorkTasks.ToListAsync();

        public async Task InsertWorkTask(GetWorkItemFullDTO workTask)
        {
            await _databaseContext.WorkTasks.AddAsync(workTask.Adapt<WorkTask>());
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<bool> TaskExistenceCheckByUrl(string url) => await _databaseContext.WorkTasks.AnyAsync(task => task.UrlLink.ToLower() == url.ToLower());
    }
}
