using DatabaseLayer;
using DatabaseLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace APILayer.Queries
{
    public interface ITaskingQueries
    {
        Task<List<WorkTask>> GetAllTasks();
    }

    public class TaskingQueries : ITaskingQueries
    {
        public readonly DatabaseContext _databaseContext;
        public TaskingQueries(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<WorkTask>> GetAllTasks() => await _databaseContext.WorkTasks.ToListAsync();
    }
}
