using System.ComponentModel.DataAnnotations;

namespace MVCAppEntityFramework.Models;

public class BookModel
{
    public int Id { get; set; }

    [Required]
    [RegularExpression("^(?:ISBN(?:-13)?:?\\ )?(?=[0-9]{13}$|(?=(?:[0-9]+[-\\ ]){4})[-\\ 0-9]{17}$)97[89][-\\ ]?[0-9]{1,5}[-\\ ]?[0-9]+[-\\ ]?[0-9]+[-\\ ]?[0-9]$",
       ErrorMessage = "{0} must be a valid ISBN code")]
    public string? ISBN { get; set; }

    [Required]
    [StringLength(maximumLength: 50)]
    public string? Title { get; set; }

    [Required]
    [StringLength(maximumLength: 50)]
    public string? Author { get; set; }

    [Required]
    public string? Publisher { get; set; }

    [Required]
    public string? Description { get; set; }

    public string? Category { get; set; }

    [Required]
    [Range(0, 200)]
    [Display(Name = "Total Pages")]
    public int TotalPages { get; set; }

    [Required]
    public double Price { get; set; }
}
