

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DatabaseLayer.Entities;

public class WorkTask
{
    public Guid Id { get; set; }
    public string AssigneeName { get; set; }
    public string Title { get; set; }
    public long EpochCreateDate { get; set; }
    public long EpochUpdatedDate { get; set; }
    public string Description { get; set; }
    public string UrlLink { get; set; }
    public string SiteSource { get; set; }
    public long EpochDueDate { get; set; }
    public long EpochStartDate { get; set; }
    public string Status { get; set; }
    public WorkTaskType TaskType { get; set; }
    public Guid TaskTypeId { get; set; }
}

public class WorkTaskConfig : IEntityTypeConfiguration<WorkTask>
{
    public void Configure(EntityTypeBuilder<WorkTask> builder)
    {
        builder.Property(prop => prop.Title).IsRequired();
        builder.Property(prop => prop.SiteSource).IsRequired();
        builder.ToTable(nameof(WorkTask).ToLower(), Schemas.Tasking);
    }
}
