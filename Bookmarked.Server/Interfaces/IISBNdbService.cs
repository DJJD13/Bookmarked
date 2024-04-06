using Bookmarked.Server.Models;

namespace Bookmarked.Server.Interfaces
{
    public interface IISBNdbService
    {
        Task<Book?> FindBookByISBNAsync(string isbn);
    }
}
