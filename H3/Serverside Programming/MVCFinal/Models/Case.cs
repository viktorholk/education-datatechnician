namespace MVCFinal.Models;

public enum CaseType{
    Bug,
    Feature
}

public class Case
{
    public int Id { get; set; }

    public CaseType Type { get; set; }

    public string? Description { get; set; }

    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public int ClientId { get; set; }

    public Client? Client { get; set; }

    public ICollection<ResourceTask>? Resources { get; set; }

}
