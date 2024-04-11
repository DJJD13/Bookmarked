using System.Security.Claims;
using Bookmarked.Server.Controllers;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        var userClaims = new List<Claim>
        {
            new(ClaimTypes.GivenName, "testuser")
        };
        var identity = new ClaimsIdentity(userClaims, "TestAuthentication");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext
        {
            User = claimsPrincipal
        };
        var controllerContext = new ControllerContext { HttpContext = httpContext };

        _mockBookshelfRepo = new Mock<IBookshelfRepository>();
        _mockBookRepo = new Mock<IBookRepository>();
        _mockIsbnService = new Mock<IISBNdbService>();
        _mockUserManager = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null!, null!, null!, null!, null!, null!, null!, null!);
        _controller = new BookshelfController(
            _mockUserManager.Object,
            _mockBookRepo.Object,
            _mockBookshelfRepo.Object,
            _mockIsbnService.Object)
        {
            ControllerContext = controllerContext
        };
    }
    
    [Fact]
    public async void GetUserBookshelf_InvalidUser_ReturnsBadRequestResponse()
    {
        // Arrange
        _mockUserManager.Setup(repo => repo.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((AppUser?)null);

        // Act
        var result = await _controller.GetUserBookshelf();
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async void GetUserBookshelf_ValidUser_ReturnsOkResponse()
    {
        // Arrange
        var userBookshelf = new List<Book>();
        var testUser = new AppUser { UserName = "testuser" };
        _mockUserManager.Setup(repo => repo.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(testUser);
        _mockBookshelfRepo.Setup(repo => repo.GetUserBookshelf(It.IsAny<AppUser>())).ReturnsAsync(userBookshelf);

        // Act
        var result = await _controller.GetUserBookshelf();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void GetUserBookshelf_ValidUser_ResponseReturnsListOfBooks()
    {
        // Arrange
        var userBookshelf = new List<Book> { new(), new() };
        var testUser = new AppUser { UserName = "testuser" };
        _mockUserManager.Setup(repo => repo.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(testUser);
        _mockBookshelfRepo.Setup(repo => repo.GetUserBookshelf(It.IsAny<AppUser>())).ReturnsAsync(userBookshelf);
        
        // Act
        var result = await _controller.GetUserBookshelf();
        
        // Assert
        Assert.NotNull(result);
        var response = Assert.IsType<OkObjectResult>(result);
        var books = Assert.IsType<List<Book>>(response.Value);
        
        Assert.Equal(2, books.Count);
    }

    [Fact]
    public async void AddBookshelf_InvalidUser_ReturnsBadRequestResponse()
    {
        // Arrange
        _mockUserManager.Setup(repo => repo.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((AppUser?)null);
        
        // Act
        var result = await _controller.AddBookshelf("123");
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async void AddBookshelf_ValidUserInvalidIsbn_ReturnsBadRequestResponse()
    {
        // Arrange
        var testUser = new AppUser { UserName = "testuser" };
        _mockUserManager.Setup(repo => repo.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(testUser);
        _mockBookRepo.Setup(repo => repo.GetByIsbnAsync(It.IsAny<string>())).ReturnsAsync((Book?)null);
        _mockIsbnService.Setup(service => service.FindBookByISBNAsync(It.IsAny<string>())).ReturnsAsync((Book?)null);
        
        // Act
        var result = await _controller.AddBookshelf("123");
            
        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async void AddBookshelf_ValidUserDuplicateIsbn_ReturnsBadRequestResponse()
    {
        // Arrange
        var testUser = new AppUser { UserName = "testuser" };
        var testBook = new Book();
        var testBookList = new List<Book> { new(){ Isbn = "123"} };
        _mockUserManager.Setup(repo => repo.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(testUser);
        _mockBookRepo.Setup(repo => repo.GetByIsbnAsync(It.IsAny<string>())).ReturnsAsync(testBook);
        _mockBookshelfRepo.Setup(repo => repo.GetUserBookshelf(It.IsAny<AppUser>())).ReturnsAsync(testBookList);
        // Act
        var result = await _controller.AddBookshelf("123");
                
        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async void AddBookshelf_ValidUserValidIsbn_ReturnsCreatedResult()
    {
        // Arrange
        var testUser = new AppUser { UserName = "testuser" };
        var testBook = new Book();
        var testBookList = new List<Book> { new(){ Isbn = "123"} };
        _mockUserManager.Setup(repo => repo.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(testUser);
        _mockBookRepo.Setup(repo => repo.GetByIsbnAsync(It.IsAny<string>())).ReturnsAsync(testBook);
        _mockBookshelfRepo.Setup(repo => repo.GetUserBookshelf(It.IsAny<AppUser>())).ReturnsAsync(testBookList);
        // Act
        var result = await _controller.AddBookshelf("12345");
                    
        // Assert
        Assert.NotNull(result);
        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public async void Delete_InvalidUser_ReturnsBadRequest()
    {
        // Arrange
        _mockUserManager.Setup(repo => repo.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((AppUser?)null);
        
        // Act
        var result = await _controller.Delete("1234");
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async void Delete_IsbnNotInBookshelf_ReturnsBadRequest()
    {
        // Arrange
        var testUser = new AppUser { UserName = "testuser" };
        var testBookshelf = new List<Book> { new() { Isbn = "123" }, new() { Isbn = "432" } };
        _mockUserManager.Setup(repo => repo.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(testUser);
        _mockBookshelfRepo.Setup(repo => repo.GetUserBookshelf(It.IsAny<AppUser>())).ReturnsAsync(testBookshelf);
        
        // Act
        var result = await _controller.Delete("1234");
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public async void Delete_IsbnInBookshelf_ReturnsOkResponse()
    {
        // Arrange
        var testUser = new AppUser { UserName = "testuser" };
        var testBookshelf = new List<Book> { new() { Isbn = "123" }, new() { Isbn = "432" } };
        _mockUserManager.Setup(repo => repo.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(testUser);
        _mockBookshelfRepo.Setup(repo => repo.GetUserBookshelf(It.IsAny<AppUser>())).ReturnsAsync(testBookshelf);
        
        // Act
        var result = await _controller.Delete("123");
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkResult>(result);
    }
}