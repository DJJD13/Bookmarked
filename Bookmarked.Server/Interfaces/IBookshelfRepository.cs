using Bookmarked.Server.Models;

namespace Bookmarked.Server.Interfaces
{
    public interface IBookshelfRepository
    {
        Task<List<Bookshelf>> GetUserBookshelf(AppUser user);
        Task<Bookshelf> CreateAsync(Bookshelf bookshelf);
        Task<Bookshelf?> UpdateAsync(AppUser appUser, string isbn, int status, int pagesRead);
        Task<Bookshelf?> DeleteBookshelf(AppUser appUser, string isbn);
    }
}
