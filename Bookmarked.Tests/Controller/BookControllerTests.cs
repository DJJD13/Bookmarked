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
   private readonly Mock<IISBNdbService> _mockIsbnService;
   private readonly BookController _controller;

   public BookControllerTests()
   {
      _mockRepo = new Mock<IBookRepository>();
      _mockIsbnService = new Mock<IISBNdbService>();
      _controller = new BookController(_mockRepo.Object, _mockIsbnService.Object);
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
   public async void GetById_InvalidIdPassed_ReturnsNotFoundResponse()
   {
      _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
         .ReturnsAsync((Book?)null);

      var result = await _controller.GetById(999);
      
      Assert.NotNull(result);
      Assert.IsType<NotFoundResult>(result);
   }
   
   [Fact]
   public async void GetById_ValidIdPassed_ReturnsOkResponse()
   {
      var booksPlaceholder = new Book();
      _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
         .ReturnsAsync(booksPlaceholder);

      var result = await _controller.GetById(1);

      Assert.IsType<OkObjectResult>(result);
   }
    
   [Fact]
   public async void GetById_ValidIdPassed_ReturnsSingleBookDto()
   {
      var booksPlaceholder = new Book { Id = 1 };
      _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
         .ReturnsAsync(booksPlaceholder);
    
      var result = await _controller.GetById(1);
    
      var response = Assert.IsType<OkObjectResult>(result);
      var book = Assert.IsType<BookDto>(response.Value);
      Assert.Equal(booksPlaceholder.Id, book.Id);
   }

   [Fact]
   public async void Create_InvalidObjectPassed_ReturnsBadRequest()
   {
      _controller.ModelState.AddModelError("Title", "Title is required");

      var book = new CreateBookRequestDto { Author = "Test" };

      var result = await _controller.Create(book);

      Assert.IsType<BadRequestObjectResult>(result);
   }

   [Fact]
   public async void Create_InvalidObjectPassed_CreateBookNeverExecutes()
   {
      _controller.ModelState.AddModelError("Title", "Title is required");

      var book = new CreateBookRequestDto { Author = "Test" };

      await _controller.Create(book);

      _mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Book>()), Times.Never);
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

   [Fact]
   public async void Update_InvalidIdPassed_ReturnsNotFoundResult()
   {
      UpdateBookRequestDto testBook = new()
      {
         Title = "Test"
      };

      _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<UpdateBookRequestDto>()))
         .ReturnsAsync((Book?)null);

      var result = await _controller.Update(888, testBook);

      Assert.IsType<NotFoundResult>(result);
      _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<UpdateBookRequestDto>()), Times.Once);
   }

   [Fact]
   public async void Update_InvalidObjectPassed_ReturnsBadRequestResponse()
   {
      _controller.ModelState.AddModelError("Isbn", "Isbn must be at least 10 characters");

      UpdateBookRequestDto book = new() { Isbn = "111" };

      var result = await _controller.Update(1, book);

      Assert.IsType<BadRequestObjectResult>(result);
   }
   
   [Fact]
   public async void Update_InvalidObjectPassed_UpdateBookNeverExecutes()
   {
      _controller.ModelState.AddModelError("Isbn", "Isbn must be at least 10 characters");
  
      UpdateBookRequestDto book = new() { Isbn = "111" };
  
      await _controller.Update(1, book);
  
      _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<UpdateBookRequestDto>()), Times.Never);
   } 
   
   [Fact]
   public async void Update_ValidIdAndObjectPassed_ReturnsOkResponse()
   {
      UpdateBookRequestDto testItem = new()
      {
         Title = "Test",
         Author = "Smith, Test",
         Isbn = "1234567890123",
         DatePublished = DateTime.Now
      };
         
      Book existingBook = new()
      {
         Id = 1,
         Title = "Test",
         Author = "Smith, Test",
         Isbn = "1234567890123",
         DatePublished = DateTime.Now
      };

      _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<UpdateBookRequestDto>()))
         .ReturnsAsync(existingBook);
   
      var result = await _controller.Update(1, testItem);

      Assert.IsType<OkObjectResult>(result);
   }

   [Fact]
   public async void Update_ValidIdAndObjectPassed_ReturnedResponseHasItem()
   {
      UpdateBookRequestDto testItem = new()
      {
         Title = "Test",
         Author = "Smith, Test",
         Isbn = "1234567890123",
         DatePublished = DateTime.Now
      };
               
      Book existingBook = new()
      {
         Id = 1,
         Title = "Test",
         Author = "Smith, Test",
         Isbn = "1234567890123",
         DatePublished = DateTime.Now
      };
      
      _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<UpdateBookRequestDto>()))
         .ReturnsAsync(existingBook);
         
      var result = await _controller.Update(1, testItem);

      var response = Assert.IsType<OkObjectResult>(result);
      var bookRes = Assert.IsType<BookDto>(response.Value);
      
      Assert.Equal(existingBook.Id, bookRes.Id);
   }

   [Fact]
   public async void Delete_InvalidIdPassed_ReturnsNotFoundResponse()
   {
      _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
         .ReturnsAsync((Book?)null);

      var result = await _controller.Delete(999);

      Assert.IsType<NotFoundResult>(result);
   }

   [Fact]
   public async void Delete_ValidIdPassed_ReturnsOkResponse()
   {
      var testBook = new Book();
      _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
         .ReturnsAsync(testBook);

      var result = await _controller.Delete(1);

      Assert.IsType<OkObjectResult>(result);
   }

   [Fact]
   public async void Delete_ValidIdPassed_ReturnedResponseHasItem()
   {
      var testBook = new Book() { Id = 1 };
      _mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
         .ReturnsAsync(testBook);

      var result = await _controller.Delete(1);

      var response = Assert.IsType<OkObjectResult>(result);
      var bookItem = Assert.IsType<BookDto>(response.Value);

      Assert.Equal(bookItem.Id, testBook.Id);
   }
}