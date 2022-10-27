
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Document
    {
        public int Id { get; set; }

        public int? ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client? Client { get; set; }
        public string? Title { get; set; }
        public string? EncodedData { get; set; }
        public string? Attributes { get; set; }
    }
}
