using Microsoft.AspNetCore.Identity;

namespace Bookmarked.Server.Models
{
    public class AppUser : IdentityUser
    {
        public List<Bookshelf> Bookshelves { get; set; } = [];
    }
}
