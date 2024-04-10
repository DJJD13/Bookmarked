using Bookmarked.Server.Controllers;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Models;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Bookmarked.Tests.Controller;

public class BookshelfControllerTests
{
    private readonly Mock<IBookshelfRepository> _mockBookshelfRepo;
    private readonly Mock<IBookRepository> _mockBookRepo;
    private readonly Mock<IISBNdbService> _mockIsbnService;
    private readonly Mock<UserManager<AppUser>> _mockUserManager; 
    private readonly BookshelfController _controller;

    public BookshelfControllerTests()
    {
        _mockBookshelfRepo = new Mock<IBookshelfRepository>();
        _mockBookRepo = new Mock<IBookRepository>();
        _mockIsbnService = new Mock<IISBNdbService>();
        _mockUserManager = new Mock<UserManager<AppUser>>();
        _controller = new BookshelfController(
            _mockUserManager.Object, 
            _mockBookRepo.Object, 
            _mockBookshelfRepo.Object, 
            _mockIsbnService.Object);
    }
    
    
}