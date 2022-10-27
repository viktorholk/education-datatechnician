namespace API.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string? Username { get; set; }

        public ICollection<Document>? Documents { get; set; }
    }
}
