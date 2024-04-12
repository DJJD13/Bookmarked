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
}