using Bookmarked.Server.Models;

namespace Bookmarked.Server.Interfaces
{
    public interface IBookshelfRepository
    {
        Task<List<Bookshelf>> GetUserBookshelf(AppUser user);
        Task<Bookshelf> CreateAsync(Bookshelf bookshelf);
        Task<Bookshelf?> UpdateStatusAsync(AppUser appUser, string isbn, int status);
        Task<Bookshelf?> DeleteBookshelf(AppUser appUser, string isbn);
    }
}
