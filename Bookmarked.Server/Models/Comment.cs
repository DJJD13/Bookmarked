using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookmarked.Server.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? BookId { get; set; }
        public Book? Book { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; }
    }
}
