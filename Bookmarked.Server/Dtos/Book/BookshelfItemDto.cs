namespace Bookmarked.Server.Dtos.Book;

public class BookshelfItemDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Isbn { get; set; }
    public string CoverImage { get; set; }
    public int ReadingStatus { get; set; }
    public int PagesRead { get; set; }
    public int TotalPages { get; set; }
}