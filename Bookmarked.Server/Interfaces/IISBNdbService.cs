using Bookmarked.Server.Dtos.Book;
using Bookmarked.Server.Models;

namespace Bookmarked.Server.Interfaces
{
    public interface IISBNdbService
    {
        Task<IsbnBook?> FindBookByISBNAsync(string isbn);
        Task<List<IsbnBook>> FindBooksAsync(string title);
        Task<List<IsbnBook>> FindBooksByAuthorAsync(string query);
    }
}
