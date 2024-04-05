using Bookmarked.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookmarked.Server.Data
{
    public class ApplicationDbContext(DbContextOptions dbContextOptions) : IdentityDbContext<AppUser>(dbContextOptions)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Bookshelf> Bookshelves { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Declare foreign keys
            builder.Entity<Bookshelf>(x => x.HasKey(b => new { b.AppUserId, b.BookId }));

            builder.Entity<Bookshelf>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Bookshelves)
                .HasForeignKey(b => b.AppUserId);

            builder.Entity<Bookshelf>()
                .HasOne(u => u.Book)
                .WithMany(u => u.Bookshelves)
                .HasForeignKey(b => b.BookId);

            List<IdentityRole> roles =
            [
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            ];
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
