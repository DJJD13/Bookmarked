using Bookmarked.Server.Dtos.Book;
using Bookmarked.Server.Helpers;
using Bookmarked.Server.Models;

namespace Bookmarked.Server.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(QueryObject query);
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetByIsbnAsync(string isbn);
        Task<Book> CreateAsync(Book bookModel);
        Task<Book?> UpdateAsync(int id, UpdateBookRequestDto bookDto);
        Task<Book?> DeleteAsync(int id);
        Task<bool> BookExists(int id);
    }
}
