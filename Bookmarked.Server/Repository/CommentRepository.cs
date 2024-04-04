using Bookmarked.Server.Data;
using Bookmarked.Server.Dtos.Comment;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookmarked.Server.Repository
{
    public class CommentRepository(ApplicationDbContext context) : ICommentRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentDto.Title;
            existingComment.Content = commentDto.Content;
            await _context.SaveChangesAsync();

            return existingComment;
            
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (commentModel == null)
            {
                return null;
            }

            _context.Remove(commentModel);

            await _context.SaveChangesAsync();

            return commentModel;
        }
    }
}
