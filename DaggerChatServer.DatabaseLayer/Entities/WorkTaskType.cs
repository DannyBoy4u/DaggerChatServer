using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DatabaseLayer.Entities;
public class WorkTaskType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class WorkTaskTypeConfig : IEntityTypeConfiguration<WorkTaskType>
{
    public void Configure(EntityTypeBuilder<WorkTaskType> builder)
    {
        builder.Property(prop => prop.Name).IsRequired();
        builder.ToTable(nameof(WorkTaskType).ToLower(), Schemas.Tasking);
    }
}
