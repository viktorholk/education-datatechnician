
namespace API.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? EncodedData { get; set; }

        public ICollection<AdditionalAtrribute> Atributes { get; set; }
    }
}
