using System.ComponentModel.DataAnnotations.Schema;

namespace Bookmarked.Server.Dtos.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public DateTime DatePublished { get; set; } = DateTime.MinValue;
        public decimal Msrp { get; set; }
        public int Pages { get; set; }
        public string CoverImage { get; set; } = string.Empty;
    }
}
