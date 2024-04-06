using Bookmarked.Server.Extensions;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bookmarked.Server.Controllers
{
    [Route("api/bookshelf")]
    [ApiController]
    public class BookshelfController(
        UserManager<AppUser> userManager,
        IBookRepository bookRepo,
        IBookshelfRepository bookshelfRepo,
        IISBNdbService isbndbService) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IBookRepository _bookRepo = bookRepo;
        private readonly IBookshelfRepository _bookshelfRepo = bookshelfRepo;
        private readonly IISBNdbService _isbndbService = isbndbService;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserBookshelf()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) return BadRequest("User not found");

            var userBookshelf = await _bookshelfRepo.GetUserBookshelf(appUser);

            return Ok(userBookshelf);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBookshelf(string isbn)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) return BadRequest("User not found");

            var book = await _bookRepo.GetByIsbnAsync(isbn);

            if (book == null)
            {
                book = await _isbndbService.FindBookByISBNAsync(isbn);
                if (book == null) return BadRequest("Book was not found");

                await _bookRepo.CreateAsync(book);
            }

            var userBookshelf = await _bookshelfRepo.GetUserBookshelf(appUser);

            if (userBookshelf.Any(e => e.Isbn == isbn)) return BadRequest("Cannot add same book to bookshelf");

            var bookshelfModel = new Bookshelf
            {
                AppUserId = appUser.Id,
                BookId = book.Id
            };

            await _bookshelfRepo.CreateAsync(bookshelfModel);

            return bookshelfModel != null ? Created() : StatusCode(500, "Could not create bookshelf");
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(string isbn)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) return BadRequest("User not found");

            var userBookshelf = await _bookshelfRepo.GetUserBookshelf(appUser);

            var filteredBook = userBookshelf.Where(b => b.Isbn == isbn).ToList();

            if (filteredBook.Count == 1)
            {
                await _bookshelfRepo.DeleteBookshelf(appUser, isbn);
            }
            else
            {
                return BadRequest("Book is not in your bookshelf");
            }

            return Ok();
        }
    }
}
