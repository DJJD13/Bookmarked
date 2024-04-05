using Bookmarked.Server.Data;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookmarked.Server.Repository
{
    public class BookshelfRepository(ApplicationDbContext context) : IBookshelfRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<List<Book>> GetUserBookshelf(AppUser user)
        {
            return await _context.Bookshelves.Where(u => u.AppUserId == user.Id)
                .Select(book => new Book
                {
                    Id = book.BookId,
                    Title = book.Book.Title,
                    Author = book.Book.Author,
                    Synopsis = book.Book.Synopsis,
                    DatePublished = book.Book.DatePublished,
                    Isbn = book.Book.Isbn,
                    CoverImage = book.Book.CoverImage,
                    Msrp = book.Book.Msrp,
                    Pages = book.Book.Pages
                }).ToListAsync();
        }

        public async Task<Bookshelf> CreateAsync(Bookshelf bookshelf)
        {
            await _context.Bookshelves.AddAsync(bookshelf);
            await _context.SaveChangesAsync();
            return bookshelf;
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
