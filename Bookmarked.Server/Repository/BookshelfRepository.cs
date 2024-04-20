using Bookmarked.Server.Data;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookmarked.Server.Repository
{
    public class BookshelfRepository(ApplicationDbContext context) : IBookshelfRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<List<Bookshelf>> GetUserBookshelf(AppUser user)
        {
            return await _context.Bookshelves.Include(bookshelf => bookshelf.Book)
                .ThenInclude(b => b.Comments)
                .Where(u => u.AppUserId == user.Id)
                .ToListAsync();
        }

        public async Task<Bookshelf> CreateAsync(Bookshelf bookshelf)
        {
            await _context.Bookshelves.AddAsync(bookshelf);
            await _context.SaveChangesAsync();
            return bookshelf;
        }

        public async Task<Bookshelf?> UpdateStatusAsync(AppUser appUser, string isbn, int status)
        {
            var existingBookshelf = await _context.Bookshelves.FirstOrDefaultAsync(bookshelf =>
                bookshelf.AppUserId == appUser.Id && bookshelf.Book.Isbn == isbn);

            if (existingBookshelf == null)
            {
                return null;
            }

            existingBookshelf.ReadingStatus = status;
            await _context.SaveChangesAsync();
            return existingBookshelf;
        }

        public async Task<Bookshelf?> DeleteBookshelf(AppUser appUser, string isbn)
        {
            var bookshelfModel = await _context.Bookshelves
                .FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Book.Isbn == isbn);

            if (bookshelfModel == null)
            {
                return null;
            }

            _context.Bookshelves.Remove(bookshelfModel);
            await _context.SaveChangesAsync();
            return bookshelfModel;
        }
    }
}
