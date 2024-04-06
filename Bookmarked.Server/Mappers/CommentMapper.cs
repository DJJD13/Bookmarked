using Bookmarked.Server.Dtos.Comment;
using Bookmarked.Server.Models;

namespace Bookmarked.Server.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                CreatedBy = commentModel.AppUser.UserName,
                BookId = commentModel.BookId
            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentRequestDto commentDto, int bookId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                BookId = bookId
            };
        }
    }
}
