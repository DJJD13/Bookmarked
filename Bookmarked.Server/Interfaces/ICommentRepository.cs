using Bookmarked.Server.Dtos.Comment;
using Bookmarked.Server.Helpers;
using Bookmarked.Server.Models;

namespace Bookmarked.Server.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentModel);
        Task<Comment?> DeleteAsync(int id);
    }
}
