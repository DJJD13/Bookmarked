using Bookmarked.Server.Data;
using Bookmarked.Server.Dtos.Book;
using Bookmarked.Server.Helpers;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookmarked.Server.Repository
{
    public class BookRepository(ApplicationDbContext context) : IBookRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<List<Book>> GetAllAsync(QueryObject query)
        {
            var books = _context.Books.Include(c => c.Comments).AsQueryable();

            if (query.Title.HasValue())
            {
                books = books.Where(b => b.Title.Contains(query.Title!));
            }

            if (query.Author.HasValue())
            {
                books = books.Where(b => b.Author.Contains(query.Author!));
            }

            if (query.SortBy.HasValue())
            {
                if (query.SortBy!.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    books = query.IsDescending ? books.OrderByDescending(b => b.Title) : books.OrderBy(b => b.Title);
                }

                if (query.SortBy!.Equals("Author", StringComparison.OrdinalIgnoreCase))
                {
                    books = query.IsDescending ? books.OrderByDescending(b => b.Author) : books.OrderBy(b => b.Author);
                }

            }

            var skipNum = (query.PageNumber - 1) * query.PageSize;


            return await books.Skip(skipNum).Take(query.PageSize).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.Include(c => c.Comments).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> CreateAsync(Book bookModel)
        {
            await _context.Books.AddAsync(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
        }

        public async Task<Book?> UpdateAsync(int id, UpdateBookRequestDto bookDto)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (existingBook == null)
            {
                return null;
            }
            
            existingBook.Title = bookDto.Title;
            existingBook.Author = bookDto.Author;
            existingBook.Synopsis = bookDto.Synopsis;
            existingBook.DatePublished = bookDto.DatePublished;
            existingBook.Isbn = bookDto.Isbn;
            existingBook.Msrp = bookDto.Msrp;
            existingBook.Pages = bookDto.Pages;
            existingBook.CoverImage = bookDto.CoverImage;

            await _context.SaveChangesAsync();

            return existingBook;
        }

       
        public async Task<Book?> DeleteAsync(int id)
        {
            var bookModel = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (bookModel == null)
            {
                return null;
            }

            _context.Remove(bookModel);

            await _context.SaveChangesAsync();

            return bookModel;
        }

        public async Task<bool> BookExists(int id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }
    }
}
