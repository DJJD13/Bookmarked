using Bookmarked.Server.Data;
using Bookmarked.Server.Dtos.Book;
using Bookmarked.Server.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Bookmarked.Server.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _context.Books.ToList()
                .Select(b => b.ToBookDto());

            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book.ToBookDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateBookRequestDto bookDto)
        {
            var bookModel = bookDto.ToBookFromCreateDto();
            _context.Books.Add(bookModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = bookModel.Id }, bookModel.ToBookDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateBookRequestDto updateDto)
        {
            var bookModel = _context.Books.FirstOrDefault(b => b.Id == id);

            if (bookModel == null)
            {
                return NotFound();
            }

            bookModel.Title = updateDto.Title;
            bookModel.Author = updateDto.Author;
            bookModel.Synopsis = updateDto.Synopsis;
            bookModel.DatePublished = updateDto.DatePublished;
            bookModel.Isbn = updateDto.Isbn;
            bookModel.Msrp = updateDto.Msrp;
            bookModel.Pages = updateDto.Pages;
            bookModel.CoverImage = updateDto.CoverImage;

            _context.SaveChanges();

            return Ok(bookModel.ToBookDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var bookModel = _context.Books.FirstOrDefault(b => b.Id == id);

            if (bookModel == null)
            {
                return NotFound();
            }

            _context.Remove(bookModel);

            _context.SaveChanges();

            return Ok(bookModel.ToBookDto());
        }
    }
}
