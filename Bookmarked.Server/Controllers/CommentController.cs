using Bookmarked.Server.Dtos.Comment;
using Bookmarked.Server.Extensions;
using Bookmarked.Server.Helpers;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Mappers;
using Bookmarked.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bookmarked.Server.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController(ICommentRepository commentRepo, IBookRepository bookRepo, UserManager<AppUser> userManager, IISBNdbService isbndbService) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly ICommentRepository _commentRepo = commentRepo;
        private readonly IBookRepository _bookRepo = bookRepo;
        private readonly IISBNdbService _isbndbService = isbndbService;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject queryObject)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comments = await _commentRepo.GetAllAsync(queryObject);
            return Ok(comments.Select(c => c.ToCommentDto()).ToList());
        }

        [HttpGet]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        [Authorize]
        [Route("{isbn}")]
        public async Task<IActionResult> Create([FromRoute] string isbn, [FromBody] CreateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var book = await _bookRepo.GetByIsbnAsync(isbn);

            if (book == null)
            {
                var isbnBook = await _isbndbService.FindBookByISBNAsync(isbn);
                if (isbnBook == null) return BadRequest("Book was not found");
                book = isbnBook.ToBookFromIsbnBook();
                await _bookRepo.CreateAsync(book);
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) return BadRequest("User not found");

            var commentModel = commentDto.ToCommentFromCreate(book.Id);
            commentModel.AppUserId = appUser.Id;
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var commentModel = await _commentRepo.UpdateAsync(id, commentDto);

            if (commentModel == null)
            {
                return NotFound();
            }

            return Ok(commentModel.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var commentModel = await _commentRepo.DeleteAsync(id);

            if (commentModel == null)
            {
                return NotFound();
            }

            return Ok(commentModel.ToCommentDto());
        }
    }
}
