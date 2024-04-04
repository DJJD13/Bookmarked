using System.ComponentModel.DataAnnotations;

namespace Bookmarked.Server.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters long")]
        [MaxLength(100, ErrorMessage = "Title cannot be over 100 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters long")]
        [MaxLength(500, ErrorMessage = "Content cannot be over 500 characters")]
        public string Content { get; set; } = string.Empty;

    }
}
