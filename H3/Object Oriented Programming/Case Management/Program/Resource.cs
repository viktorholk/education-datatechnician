public class Resource
{

    public enum ResourceStatus
    {
        pending,
        ongoing,
        completed
    }

    public ResourceStatus Status {get;set;}
    public string Description{get;set;}

    public int spentHours{get; private set;}

    public Resource(string description)
    {
        this.Description = description;

        // Set defaults
        this.Status = ResourceStatus.pending;
        this.spentHours = 0;
    }

    public Resource(string description, ResourceStatus status, int spentHours)
    {
        this.Description = description;
        this.Status = status;
        this.spentHours = spentHours;
    }

    public int GetSpentHours()
    {
        return this.spentHours;
    }

    public void UpdateStatus(ResourceStatus status)
    {
        this.Status = status;
    }

    public void AddHours(int hours)
    {
        this.spentHours += hours;
    }

    public override string ToString()
    {
        return $"Status: {this.Status}, Description: {this.Description}, Spent Hours: {this.spentHours}";
    }

}
