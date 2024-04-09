using System.Collections;
using Bookmarked.Server.Controllers;
using Bookmarked.Server.Dtos.Book;
using Bookmarked.Server.Helpers;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Bookmarked.Tests.Controller;

public class BookControllerTests
{
   private readonly Mock<IBookRepository> _mockRepo;
   private readonly BookController _controller;

   public BookControllerTests()
   {
      _mockRepo = new Mock<IBookRepository>();
      _controller = new BookController(_mockRepo.Object);
   }

   [Fact]
   public async void GetAll_ActionExecutes_ReturnsOkResponse()
   {
      var books = new List<Book>();
      _mockRepo.Setup(repo => repo.GetAllAsync(It.IsAny<QueryObject>()))
         .ReturnsAsync(books);

      var result = await _controller.GetAll(new QueryObject());

      Assert.IsType<OkObjectResult>(result);
   }
   
   [Fact]
   public async void GetAll_ActionExecutes_ReturnsExactNumberOfBooks()
   {
      var booksPlaceholder = new List<Book>(){ new(), new()};
      _mockRepo.Setup(repo => repo.GetAllAsync(It.IsAny<QueryObject>()))
         .ReturnsAsync(booksPlaceholder);
   
      var result = await _controller.GetAll(new QueryObject());

      var response = Assert.IsType<OkObjectResult>(result);
      var books = Assert.IsType<List<BookDto>>(response.Value);
      Assert.Equal(2, books.Count);
   }
   
   [Fact]
   public async void GetById_ActionExecutes_ReturnsOkResponse()
   {
      var booksPlaceholder = new Book();
      _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
         .ReturnsAsync(booksPlaceholder);

      var result = await _controller.GetById(1);

      Assert.IsType<OkObjectResult>(result);
   }
    
   [Fact]
   public async void GetById_ActionExecutes_ReturnsSingleBookDto()
   {
      var booksPlaceholder = new Book(){ Id = 1 };
      _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
         .ReturnsAsync(booksPlaceholder);
    
      var result = await _controller.GetById(1);
    
      var response = Assert.IsType<OkObjectResult>(result);
      var book = Assert.IsType<BookDto>(response.Value);
      Assert.Equal(booksPlaceholder.Id, book.Id);
   }

   [Fact]
   public async void Create_ValidObjectPassed_ReturnsCreatedResponse()
   {
      CreateBookRequestDto testItem = new()
      {
         Title = "Test",
         Author = "Smith, Test",
         Isbn = "1234567890123",
         DatePublished = DateTime.Now
      };

      var createdResponse = await _controller.Create(testItem);

      Assert.IsType<CreatedAtActionResult>(createdResponse);
   }

   [Fact]
   public async void Create_ValidObjectPassed_ReturnedResponseHasItem()
   {
      CreateBookRequestDto testItem = new()
      {
         Title = "Test",
         Author = "Smith, Test",
         Isbn = "1234567890123",
         DatePublished = DateTime.Now
      };

      var createdResponse = await _controller.Create(testItem) as CreatedAtActionResult;
      var book = createdResponse!.Value as BookDto;

      Assert.IsType<BookDto>(book);
      Assert.Equal("Test", book.Title);
   }
}