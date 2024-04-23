using System.ComponentModel.DataAnnotations.Schema;

namespace Bookmarked.Server.Models
{
    [Table("Bookshelves")]
    public class Bookshelf
    {
        public string AppUserId { get; set; }
        public int BookId { get; set; }
        public int ReadingStatus { get; set; }
        public int PagesRead { get; set; }
        public AppUser AppUser { get; set; }
        public Book Book { get; set; }
    }
}
