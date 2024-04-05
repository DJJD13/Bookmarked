﻿using Bookmarked.Server.Models;

namespace Bookmarked.Server.Interfaces
{
    public interface IBookshelfRepository
    {
        Task<List<Book>> GetUserBookshelf(AppUser user);
        Task<Bookshelf> CreateAsync(Bookshelf bookshelf);
        Task<Bookshelf?> DeleteBookshelf(AppUser appUser, string isbn);
    }
}