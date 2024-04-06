using Bookmarked.Server.Dtos.Book;
using Bookmarked.Server.Models;

namespace Bookmarked.Server.Mappers
{
    public static class BookMappers
    {
        public static BookDto ToBookDto(this Book bookModel)
        {
            return new BookDto()
            {
                Id = bookModel.Id,
                Title = bookModel.Title,
                Author = bookModel.Author,
                Isbn = bookModel.Isbn,
                Synopsis = bookModel.Synopsis,
                DatePublished = bookModel.DatePublished,
                Msrp = bookModel.Msrp,
                Pages = bookModel.Pages,
                CoverImage = bookModel.CoverImage,
                Comments = bookModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Book ToBookFromCreateDto(this CreateBookRequestDto bookDto)
        {
            return new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Synopsis = bookDto.Synopsis,
                DatePublished = bookDto.DatePublished,
                Msrp = bookDto.Msrp,
                Isbn = bookDto.Isbn,
                CoverImage = bookDto.CoverImage,
                Pages = bookDto.Pages,
            };
        }

        public static Book ToBookFromIsbnBook(this IsbnBook isbnBook)
        {
            return new Book
            {
                Title = isbnBook.title,
                Author = isbnBook.authors[0],
                Synopsis = isbnBook.synopsis,
                DatePublished = DateTime.Parse(isbnBook.date_published),
                Msrp = (decimal)isbnBook.msrp,
                Isbn = isbnBook.isbn,
                CoverImage = isbnBook.image,
                Pages = isbnBook.pages,
            };
        }

    }
}
