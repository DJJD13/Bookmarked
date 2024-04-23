using Bookmarked.Server.Dtos.Book;
using Bookmarked.Server.Models;

namespace Bookmarked.Server.Mappers;

public static class BookshelfMappers
{

    public static Book ToBookFromBookshelf(this Bookshelf bookshelfModel)
    {
        return new Book
        {
            Id = bookshelfModel.BookId,
            Title = bookshelfModel.Book.Title,
            Author = bookshelfModel.Book.Author,
            Synopsis = bookshelfModel.Book.Synopsis,
            DatePublished = bookshelfModel.Book.DatePublished,
            Isbn = bookshelfModel.Book.Isbn,
            CoverImage = bookshelfModel.Book.CoverImage,
            Msrp = bookshelfModel.Book.Msrp,
            Pages = bookshelfModel.Book.Pages
        };
    }

    public static BookshelfItemDto ToBookshelfItemDto(this Bookshelf bookshelfModel) 
    {
        return new BookshelfItemDto
        {
            Id = bookshelfModel.Book.Id,
            Title = bookshelfModel.Book.Title,
            Isbn = bookshelfModel.Book.Isbn,
            CoverImage = bookshelfModel.Book.CoverImage,
            ReadingStatus = bookshelfModel.ReadingStatus,
            PagesRead = bookshelfModel.PagesRead,
            TotalPages = bookshelfModel.Book.Pages
        };
    }
}