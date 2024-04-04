using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookmarked.Server.Dtos.Book
{
    public class CreateBookRequestDto
    {
        [Required]
        [MaxLength(300, ErrorMessage = "Title cannot be more than 300 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(300, ErrorMessage = "Author cannot be more than 300 characters")]
        public string Author { get; set; } = string.Empty;
        [Required]
        [MinLength(10, ErrorMessage = "ISBN must be at least 10 characters")]
        [MaxLength(15, ErrorMessage = "ISBN cannot be over 15 characters")]
        public string Isbn { get; set; } = string.Empty;
        [MaxLength(1000, ErrorMessage = "Synopsis cannot be over 1000 characters")]
        public string Synopsis { get; set; } = string.Empty;
        [Required]
        public DateTime DatePublished { get; set; } = DateTime.MinValue;
        [Range(0.01, 1000)]
        public decimal Msrp { get; set; }
        public int Pages { get; set; }
        public string CoverImage { get; set; } = string.Empty;
    }
}
