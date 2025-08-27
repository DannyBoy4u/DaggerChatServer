using DatabaseLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DatabaseLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }

        public virtual DbSet<WorkTaskType> WorkTaskTypes { get; set; }
        public virtual DbSet<WorkTask> WorkTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}