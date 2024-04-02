using Bookmarked.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookmarked.Server.Data
{
    public class ApplicationDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
