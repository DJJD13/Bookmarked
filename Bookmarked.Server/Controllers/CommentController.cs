using Bookmarked.Server.Dtos.Comment;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Bookmarked.Server.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController(ICommentRepository commentRepo, IBookRepository bookRepo) : ControllerBase
    {
        private readonly ICommentRepository _commentRepo = commentRepo;
        private readonly IBookRepository _bookRepo = bookRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comments = await _commentRepo.GetAllAsync();
            return Ok(comments.Select(c => c.ToCommentDto()));
        }

        [HttpGet]
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
        [Route("{bookId:int}")]
        public async Task<IActionResult> Create([FromRoute] int bookId, [FromBody] CreateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _bookRepo.BookExists(bookId))
            {
                return BadRequest("Book does not exist");
            }

            var commentModel = commentDto.ToCommentFromCreate(bookId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
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
