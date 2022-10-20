using Context;

public class Case
{
    public User Creator{get;set;}

    public enum CaseType
    {
        Feature,
        Bug
    }

    public CaseType Type{get;set;}
    public string Description{get;set;}

    public Resource.ResourceStatus Status{
        get {
            return GetStatus();
        }
    }

    public List<Resource> Resources;


    public Case(User creator, CaseType type, string description)
    {
        this.Creator = creator;
        this.Type = type;
        this.Description = description;
        this.Resources = new List<Resource>();
    }
    public Case(User creator, CaseType type, string description, List<Resource> resources)
    {
        this.Creator = creator;
        this.Type = type;
        this.Description = description;
        this.Resources = resources;
    }


    public Resource CreateResource(string description)
    {
        var newResource = new Resource(description);
        this.Resources.Add(newResource);
        StatusHandler.Write("Successfully created new resource", ApplicationContext.Status.Success);

        return newResource;

    }

    public Resource.ResourceStatus GetStatus()
    {
        // Get case status from the associated resource statuses
        if (this.Resources.All(i => i.Status == Resource.ResourceStatus.completed))
            return Resource.ResourceStatus.completed;

        if (this.Resources.Any(i => i.Status == Resource.ResourceStatus.ongoing))
            return Resource.ResourceStatus.ongoing;

        return Resource.ResourceStatus.pending;
    }

    public int GetTotalHours()
    {
        return this.Resources.Sum(i => i.GetSpentHours());
    }

    public override string ToString()
    {
        return $"Creator: {this.Creator.ToString()}, Type: {this.Type}, Description: {this.Description}, Total Hours: {GetTotalHours()}";
    }

}
