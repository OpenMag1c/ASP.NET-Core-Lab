using Business.DTO;
using Business.Interfaces;
using FakeItEasy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;
using WebAPI.Tests.Extensions;
using Xunit;

namespace WebAPI.Tests.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public async Task UpdateProfile_ReturnsOkStatusCodeAndUserDto()
        {
            // Arrange
            var validUserDto = UserControllerTestData._validUserDTO;
            var fakeUserService = A.Fake<IUserService>();
            var userController = new UserController(null, fakeUserService).WithTestUser();

            A.CallTo(() => fakeUserService.UpdateUserAsync(A<string>.Ignored, A<UserDTO>.Ignored))
                .Returns(Task.FromResult(new UserDTO()));

            // Act
            var actionResult = await userController.UpdateProfile(validUserDto);

            // Assert
            Assert.IsType<UserDTO>(actionResult.Value);
        }

        [Fact]
        public async Task UpdateProfile_ReturnsBadRequestStatusCode()
        {
            // Arrange
            var validUserDto = UserControllerTestData._validUserDTO;
            var fakeUserService = A.Fake<IUserService>();
            var userController = new UserController(null, fakeUserService).WithTestUser();

            A.CallTo(() => fakeUserService.UpdateUserAsync(A<string>.Ignored, A<UserDTO>.Ignored))
                .Returns(Task.FromResult<UserDTO>(null));

            // Act
            var actionResult = await userController.UpdateProfile(validUserDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task UpdateProfile_Authorize()
        {
            // Arrange
            var validUserDto = UserControllerTestData._validUserDTO;
            var fakeUserService = A.Fake<IUserService>();
            var userController = new UserController(null, fakeUserService).WithTestUser();
            var actualAttribute = userController.GetType().GetMethod("UpdateProfile")?
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            // Act
            await userController.UpdateProfile(validUserDto);

            // Assert
            Assert.Equal(typeof(AuthorizeAttribute), actualAttribute?[0].GetType());
        }

        [Fact]
        public async Task ChangeProfilePassword_ReturnsNoContentStatusCode()
        {
            // Arrange
            var fakeUserService = A.Fake<IUserService>();
            var userController = new UserController(null, fakeUserService).WithTestUser();

            A.CallTo(() => fakeUserService.ChangePasswordAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .Returns(Task.FromResult(true));

            // Act
            var actionResult = await userController.ChangeProfilePassword("oldPassword", "newPassword");

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task ChangeProfilePassword_ReturnsBadRequestStatusCode()
        {
            // Arrange
            var fakeUserService = A.Fake<IUserService>();
            var userController = new UserController(null, fakeUserService).WithTestUser();

            A.CallTo(() => fakeUserService.ChangePasswordAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .Returns(Task.FromResult(false));

            // Act
            var actionResult = await userController.ChangeProfilePassword("oldPassword", "newPassword");

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task ChangeProfilePassword_Authorize()
        {
            // Arrange
            var fakeUserService = A.Fake<IUserService>();
            var userController = new UserController(null, fakeUserService).WithTestUser();
            var actualAttribute = userController.GetType().GetMethod("ChangeProfilePassword")?
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            // Act
            await userController.ChangeProfilePassword("oldPassword", "newPassword");

            // Assert
            Assert.Equal(typeof(AuthorizeAttribute), actualAttribute?[0].GetType());
        }

        [Fact]
        public async Task GetProfileInfo_ReturnsOkStatusCodeAndUserDto()
        {
            // Arrange
            var fakeUserService = A.Fake<IUserService>();
            var userController = new UserController(null, fakeUserService).WithTestUser();

            A.CallTo(() => fakeUserService.GetProfileInfoAsync(null)).WithAnyArguments()
                .Returns(Task.FromResult(new UserDTO()));

            // Act
            var actionResult = await userController.GetProfileInfo();

            // Assert
            Assert.IsType<UserDTO>(actionResult.Value);
        }

        [Fact]
        public async Task GetProfileInfo_ReturnsNotFoundStatusCode()
        {
            // Arrange
            var fakeUserService = A.Fake<IUserService>();
            var userController = new UserController(null, fakeUserService).WithTestUser();

            A.CallTo(() => fakeUserService.GetProfileInfoAsync(null)).WithAnyArguments()
                .Returns(Task.FromResult<UserDTO>(null));

            // Act
            var actionResult = await userController.GetProfileInfo();

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetProfileInfo_Authorize()
        {
            // Arrange
            var fakeUserService = A.Fake<IUserService>();
            var userController = new UserController(null, fakeUserService).WithTestUser();
            var actualAttribute = userController.GetType().GetMethod("GetProfileInfo")?
                .GetCustomAttributes(typeof(AuthorizeAttribute), true);

            // Act
            await userController.GetProfileInfo();

            // Assert
            Assert.Equal(typeof(AuthorizeAttribute), actualAttribute?[0].GetType());
        }
    }
}