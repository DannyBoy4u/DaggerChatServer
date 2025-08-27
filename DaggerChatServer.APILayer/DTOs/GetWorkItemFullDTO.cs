using DatabaseLayer.Entities;

namespace APILayer.DTOs;
public class GetWorkItemFullDTO
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

