using Context;
public class Product
{

    public string Name { get; set; }
    public string Description { get; set; }

    public List<Case> Cases;

    public Product(string name, string description)
    {
        this.Name = name;
        this.Description = description;
        this.Cases = new List<Case>();
    }

    public Product(string name, string description, List<Case> cases)
    {
        this.Name = name;
        this.Description = description;
        this.Cases = cases;
    }

    public Case CreateCase(User creator, Case.CaseType type, string description)
    {
        var newCase = new Case(creator, type, description);
        this.Cases.Add(newCase);
        StatusHandler.Write("Successfully created case", ApplicationContext.Status.Success);
        return newCase;
    }

    public override string ToString()
    {
        return $"Name: {this.Name}, Description: {this.Description}";
    }
}
