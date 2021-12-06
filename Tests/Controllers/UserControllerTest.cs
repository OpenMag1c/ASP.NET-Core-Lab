using Business.DTO;
using Business.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Controllers;
using WebAPI.Tests.Extensions;
using Xunit;

namespace WebAPI.Tests.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public async Task UpdateProfileShouldReturnOkStatusCodeAndUserDTO()
        {
            // Arrange
            var validUserDto = UserControllerTestData._validUserDTO;
            var fakeUserService = A.Fake<IUserService>();
            var userController = new UserController(null, fakeUserService)
                .WithTestUser();

            A.CallTo(() => fakeUserService.UpdateUserAsync(A<string>.Ignored, A<UserDTO>.Ignored))
                .Returns(Task.FromResult(validUserDto));

            // Act
            var actionResult = await userController.UpdateProfile(validUserDto);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var returnUserDto = result.Value as UserDTO;
            Assert.Equal(validUserDto, returnUserDto);
            Assert.IsType<UserDTO>(actionResult.Value);
        }

        [Fact]
        public async Task ChangeProfilePasswordShouldReturnNoContent()
        {
            // Arrange
            var fakeUserService = A.Fake<IUserService>();
            var userController = new UserController(null, fakeUserService)
                .WithTestUser();

            A.CallTo(() => fakeUserService.ChangePasswordAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored));

            // Act
            var actionResult = await userController.ChangeProfilePassword("password", "password");

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }
    }
}