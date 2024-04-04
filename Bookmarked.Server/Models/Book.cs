using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookmarked.Server.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public DateTime DatePublished { get; set; } = DateTime.MinValue;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Msrp { get; set; }
        public int Pages { get; set; }
        public string CoverImage { get; set; } = string.Empty;

        public List<Comment> Comments { get; set; } = [];
    }
}
