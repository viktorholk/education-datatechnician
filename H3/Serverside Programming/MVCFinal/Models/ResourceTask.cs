namespace MVCFinal.Models;
public enum Status
{
    Pending,
    Ongoing,
    Completed
}
public class ResourceTask
{
    public int Id { get; set; }

    public Status Status { get; set; }

    public string? Description { get; set; }

    public int CaseId { get; set; }
    public Case? Case { get; set; }
}
