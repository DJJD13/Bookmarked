using System.Linq.Expressions;
using Bookmarked.Server.Controllers;
using Bookmarked.Server.Dtos.Account;
using Bookmarked.Server.Interfaces;
using Bookmarked.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace Bookmarked.Tests.Controller;

public class AccountControllerTests
{
   private readonly Mock<UserManager<AppUser>> _mockUserManager;
   private readonly Mock<SignInManager<AppUser>> _mockSignInManager;
   private readonly Mock<ITokenService> _mockTokenService;
   private readonly AccountController _controller;

   public AccountControllerTests()
   {
      _mockUserManager = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null!, null!, null!, null!, null!, null!, null!, null!);
      _mockTokenService = new Mock<ITokenService>();
      _mockSignInManager = new Mock<SignInManager<AppUser>>(
         _mockUserManager.Object,
         Mock.Of<IHttpContextAccessor>(),
         Mock.Of<IUserClaimsPrincipalFactory<AppUser>>(),
         null!,
         null!,
         null!,
         null!);
      _controller = new AccountController(
         _mockUserManager.Object, 
         _mockTokenService.Object, 
         _mockSignInManager.Object);
   }

   [Fact]
   public async void Login_InvalidModel_ReturnsBadRequest()
   {
      _controller.ModelState.AddModelError("Username", "Username is required");

      var result = await _controller.Login(new LoginDto());
      
      Assert.NotNull(result);
      Assert.IsType<BadRequestObjectResult>(result);
   }
}