using System.Security.Claims;
using Bookmarked.Server.Controllers;
using Bookmarked.Server.Dtos.Comment;
using Bookmarked.Server.Helpers;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Bookmarked.Tests.Controller;

public class CommentControllerTests
{
    private readonly Mock<UserManager<AppUser>> _mockUserManager;
    private readonly Mock<ICommentRepository> _mockCommentRepo;
    private readonly Mock<IBookRepository> _mockBookRepo;
    private readonly Mock<IISBNdbService> _mockISBNService;
    private readonly CommentController _controller;

    public CommentControllerTests()
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
        
        _mockUserManager = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null!, null!, null!, null!, null!, null!, null!, null!);
        _mockCommentRepo = new Mock<ICommentRepository>();
        _mockBookRepo = new Mock<IBookRepository>();
        _mockISBNService = new Mock<IISBNdbService>();
        _controller = new CommentController(
            _mockCommentRepo.Object,
            _mockBookRepo.Object,
            _mockUserManager.Object,
            _mockISBNService.Object
        )
        {
            ControllerContext = controllerContext
        };
    }

    [Fact]
    public async void GetAll_ActionExecutes_ReturnsOkResponse()
    {
        // Arrange
        var commentList = new List<Comment>();
        _mockCommentRepo.Setup(repo => repo.GetAllAsync(It.IsAny<CommentQueryObject>()))
            .ReturnsAsync(commentList);
        
        // Act
        var result = await _controller.GetAll(new CommentQueryObject());
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void GetAll_ActionExecutes_ReturnsListOfComments()
    {
        // Arrange
        var commentList = new List<Comment> { new(){ AppUser = new AppUser()}, new() { AppUser = new AppUser()} };
        _mockCommentRepo.Setup(repo => repo.GetAllAsync(It.IsAny<CommentQueryObject>()))
            .ReturnsAsync(commentList);
        
        // Act
        var result = await _controller.GetAll(new CommentQueryObject());
        
        // Assert
        Assert.NotNull(result);
        var response = Assert.IsType<OkObjectResult>(result);
        var comments = Assert.IsType<List<CommentDto>>(response.Value);
        Assert.Equal(2, comments.Count);
    }

    [Fact]
    public async void GetById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        _mockCommentRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Comment?)null);
        
        // Act
        var result = await _controller.GetById(999);
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void GetById_ValidId_ReturnsOkResponse()
    {
        // Arrange
        var comment = new Comment { Id = 1, AppUser = new AppUser() };
        _mockCommentRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(comment);
        
        // Act
        var result = await _controller.GetById(1);
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    } 
    
    [Fact]
    public async void GetById_ValidId_ReturnsCommentObject()
    {
        // Arrange
        var comment = new Comment { Id = 1, AppUser = new AppUser() };
        _mockCommentRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(comment);
        
        // Act
        var result = await _controller.GetById(1);
        
        // Assert
        Assert.NotNull(result);
        var response = Assert.IsType<OkObjectResult>(result);
        var commentDto = Assert.IsType<CommentDto>(response.Value);
        Assert.Equal(1, commentDto.Id);
    }

    [Fact]
    public async void Create_InvalidIsbn_ReturnsBadRequest()
    {
        _mockBookRepo.Setup(repo => repo.GetByIsbnAsync(It.IsAny<string>()))
            .ReturnsAsync((Book?)null);
        _mockISBNService.Setup(service => service.FindBookByISBNAsync(It.IsAny<string>()))
            .ReturnsAsync((Book?)null);

        var result = await _controller.Create("1234", new CreateCommentRequestDto());

        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async void Create_InvalidCommentDto_ReturnsBadRequest()
    {
        _controller.ModelState.AddModelError("Title", "Title is required");

        var result = await _controller.Create("1234", new CreateCommentRequestDto());

        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async void Create_InvalidUser_ReturnsBadRequest()
    {
        var book = new Book { Id = 1, Isbn = "123" };
        _mockBookRepo.Setup(repo => repo.GetByIsbnAsync(It.IsAny<string>()))
            .ReturnsAsync(book);
        _mockUserManager.Setup(repo => repo.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((AppUser?)null);

        var result = await _controller.Create("123", new CreateCommentRequestDto());

        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async void Create_ValidIdValidCommentDto_ReturnsCreatedAtActionResponse()
    {
        var book = new Book { Id = 1, Isbn = "123" };
        var commentDto = new CreateCommentRequestDto()
        {
            Title = "Title",
            Content = "Content"
        };
        var testUser = new AppUser { Id = "7fgds6g", UserName = "testuser"};
        var commentModel = new Comment
        {
            Id = 1,
            Title = "Title",
            Content = "Content",
            AppUserId = testUser.Id,
            AppUser = testUser,
            CreatedOn = DateTime.Now
        };
        _mockBookRepo.Setup(repo => repo.GetByIsbnAsync(It.IsAny<string>()))
            .ReturnsAsync(book);
        _mockUserManager.Setup(um => um.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(testUser);
        _mockCommentRepo.Setup(repo => repo.CreateAsync(It.IsAny<Comment>()))
            .ReturnsAsync(commentModel);

        var result = await _controller.Create("123", commentDto);

        Assert.NotNull(result);
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async void Create_ValidIsbnValidCommentDto_ReturnedResponseHasComment()
    {
        var book = new Book { Id = 1, Isbn = "123" };
        var commentDto = new CreateCommentRequestDto()
        {
            Title = "Title",
            Content = "Content"
        };
        var testUser = new AppUser { Id = "7fgds6g", UserName = "testuser"};
        var commentModel = new Comment
        {
            Id = 1,
            Title = "Title",
            Content = "Content",
            AppUserId = testUser.Id,
            AppUser = testUser,
            CreatedOn = DateTime.Now
        };
        _mockBookRepo.Setup(repo => repo.GetByIsbnAsync(It.IsAny<string>()))
            .ReturnsAsync(book);
        _mockUserManager.Setup(um => um.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(testUser);
        _mockCommentRepo.Setup(repo => repo.CreateAsync(It.IsAny<Comment>()))
            .ReturnsAsync(commentModel);

        var result = await _controller.Create("123", commentDto);

        Assert.NotNull(result);
        var response = Assert.IsType<CreatedAtActionResult>(result);
        var comment = Assert.IsType<CommentDto>(response.Value);

        Assert.Equal("Title", comment.Title);
    }

    [Fact]
    public async void Update_InvalidId_ReturnsNotFound()
    {
        _mockCommentRepo.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<UpdateCommentRequestDto>()))
            .ReturnsAsync((Comment?)null);

        var result = await _controller.Update(999, new UpdateCommentRequestDto());

        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void Update_ValidIdValidCommentDto_ReturnsOkResponse()
    {
        var comment = new Comment
        {
            Id = 1,
            Title = "Title",
            Content = "Content",
            AppUserId = "123",
            AppUser = new AppUser { Id = "123", UserName = "testuser" },
            CreatedOn = DateTime.Today
        };
        _mockCommentRepo.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<UpdateCommentRequestDto>()))
            .ReturnsAsync(comment);

        var result = await _controller.Update(1, new UpdateCommentRequestDto());

        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void Update_ValidIdValidCommentDto_ReturnedResponseHasComment()
    {
        var comment = new Comment
        {
            Id = 1,
            Title = "Title",
            Content = "Content",
            AppUserId = "123",
            AppUser = new AppUser { Id = "123", UserName = "testuser" },
            CreatedOn = DateTime.Today
        };
        _mockCommentRepo.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<UpdateCommentRequestDto>()))
            .ReturnsAsync(comment);

        var result = await _controller.Update(1, new UpdateCommentRequestDto());

        Assert.NotNull(result);
        var response = Assert.IsType<OkObjectResult>(result);
        var commentDto = Assert.IsType<CommentDto>(response.Value);

        Assert.Equal("Title", commentDto.Title);
    }

    [Fact]
    public async void Delete_InvalidId_ReturnsNotFound()
    {
        _mockCommentRepo.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync((Comment?)null);

        var result = await _controller.Delete(999);

        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void Delete_ValidId_ReturnsOkResponse()
    {
        var commentModel = new Comment
        {
            Id = 1,
            Title = "Title",
            Content = "Content",
            AppUser = new AppUser { Id = "1234", UserName = "User" },
            AppUserId = "1234",
            CreatedOn = DateTime.Today
        };
        _mockCommentRepo.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(commentModel);

        var result = await _controller.Delete(1);

        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async void Delete_ValidId_ReturnedResponseHasComment()
    {
        var commentModel = new Comment
        {
            Id = 1,
            Title = "Title",
            Content = "Content",
            AppUser = new AppUser { Id = "1234", UserName = "User" },
            AppUserId = "1234",
            CreatedOn = DateTime.Today
        };
        _mockCommentRepo.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(commentModel);

        var result = await _controller.Delete(1);

        Assert.NotNull(result);
        var response = Assert.IsType<OkObjectResult>(result);
        var commentDto = Assert.IsType<CommentDto>(response.Value);

        Assert.Equal("Title", commentDto.Title);
    }
}