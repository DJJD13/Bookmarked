using Bookmarked.Server.Data;
using Bookmarked.Server.Dtos.Book;
using Bookmarked.Server.Helpers;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Mappers;
using Bookmarked.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookmarked.Server.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController(IBookRepository bookRepo) : ControllerBase
    {
        private readonly IBookRepository _bookRepo = bookRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var books = await _bookRepo.GetAllAsync(query);
            var booksDto = books.Select(b => b.ToBookDto()).ToList();

            return Ok(booksDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var book = await _bookRepo.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book.ToBookDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookRequestDto bookDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var bookModel = bookDto.ToBookFromCreateDto();
            await _bookRepo.CreateAsync(bookModel);
            return CreatedAtAction(nameof(GetById), new { id = bookModel.Id }, bookModel.ToBookDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookRequestDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var bookModel = await _bookRepo.UpdateAsync(id, updateDto);

            if (bookModel == null)
            {
                return NotFound();
            }
            return Ok(bookModel.ToBookDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var bookModel = await _bookRepo.DeleteAsync(id);

            if (bookModel == null)
            {
                return NotFound();
            }
            return Ok(bookModel.ToBookDto());
        }
    }
}
