using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Business.DTO;
using Business.Interfaces;
using Business.Services;
using DAL.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace WebAPI.Tests.Services
{
    public class AuthenticationServiceTest
    {
        [Fact]
        public async Task SignInAsync_ReturnsToken()
        {
            // Arrange
            var fakeUserManager = A.Fake<UserManager<User>>();
            var token = "token";
            var fakeJwtGenerator = A.Fake<IJwtGenerator>();
            var userCredentialsDto = new Fixture().Create<UserCredentialsDTO>();
            var authenticationService = new AuthenticationService(A.Fake<IMapper>(), fakeUserManager, fakeJwtGenerator, A.Fake<SendingEmail>());
            A.CallTo(() => fakeUserManager.FindByEmailAsync(A<string>.Ignored))
                .Returns(Task.FromResult(new User()));
            A.CallTo(() => fakeUserManager.CheckPasswordAsync(A<User>.Ignored, A<string>.Ignored))
                .Returns(Task.FromResult(true));
            A.CallTo(() => fakeUserManager.IsEmailConfirmedAsync(A<User>.Ignored))
                .Returns(Task.FromResult(true));
            A.CallTo(() => fakeJwtGenerator.GenerateJwtTokenAsync(A<User>.Ignored))
                .Returns(Task.FromResult(token));

            // Act
            var result = await authenticationService.SignInAsync(userCredentialsDto);

            // Assert
            Assert.Equal(token, result);
        }

        [Fact]
        public async Task SignInAsync_ReturnsNullIfUserNotFound()
        {
            // Arrange
            var fakeUserManager = A.Fake<UserManager<User>>();
            var userCredentialsDto = new Fixture().Create<UserCredentialsDTO>();
            var authenticationService = new AuthenticationService(A.Fake<IMapper>(), fakeUserManager, A.Fake<IJwtGenerator>(), A.Fake<SendingEmail>());
            A.CallTo(() => fakeUserManager.FindByEmailAsync(A<string>.Ignored))
                .Returns(Task.FromResult<User>(null));

            // Act
            var result = await authenticationService.SignInAsync(userCredentialsDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task SignInAsync_ReturnsNullIfWrongPassword()
        {
            // Arrange
            var fakeUserManager = A.Fake<UserManager<User>>();
            var userCredentialsDto = new Fixture().Create<UserCredentialsDTO>();
            var authenticationService = new AuthenticationService(A.Fake<IMapper>(), fakeUserManager, A.Fake<IJwtGenerator>(), A.Fake<SendingEmail>());
            A.CallTo(() => fakeUserManager.FindByEmailAsync(A<string>.Ignored))
                .Returns(Task.FromResult(new User()));
            A.CallTo(() => fakeUserManager.CheckPasswordAsync(A<User>.Ignored, A<string>.Ignored))
                .Returns(Task.FromResult(false));
            A.CallTo(() => fakeUserManager.IsEmailConfirmedAsync(A<User>.Ignored))
                .Returns(Task.FromResult(true));

            // Act
            var result = await authenticationService.SignInAsync(userCredentialsDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task SignInAsync_ReturnsNullIfEmailUnconfirmed()
        {
            // Arrange
            var fakeUserManager = A.Fake<UserManager<User>>();
            var userCredentialsDto = new Fixture().Create<UserCredentialsDTO>();
            var authenticationService = new AuthenticationService(A.Fake<IMapper>(), fakeUserManager, A.Fake<IJwtGenerator>(), A.Fake<SendingEmail>());
            A.CallTo(() => fakeUserManager.FindByEmailAsync(A<string>.Ignored))
                .Returns(Task.FromResult(new User()));
            A.CallTo(() => fakeUserManager.CheckPasswordAsync(A<User>.Ignored, A<string>.Ignored))
                .Returns(Task.FromResult(true));
            A.CallTo(() => fakeUserManager.IsEmailConfirmedAsync(A<User>.Ignored))
                .Returns(Task.FromResult(false));

            // Act
            var result = await authenticationService.SignInAsync(userCredentialsDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task SignUpAsync_ReturnsTrueIfUserCreated()
        {
            // Arrange
            var fakeUserManager = A.Fake<UserManager<User>>();
            var fakeSendingEmail = A.Fake<SendingEmail>();
            var userCredentialsDto = new Fixture().Create<UserCredentialsDTO>();
            var authenticationService = new AuthenticationService(A.Fake<IMapper>(), fakeUserManager, A.Fake<IJwtGenerator>(), fakeSendingEmail);
            A.CallTo(() => fakeUserManager.CreateAsync(A<User>.Ignored, A<string>.Ignored))
                .Returns(Task.FromResult(IdentityResult.Success));
            A.CallTo(() => fakeSendingEmail.SendEmailAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored));

            // Act
            var result = await authenticationService.SignUpAsync(userCredentialsDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SignUpAsync_ReturnsFalseIfUserNotCreated()
        {
            // Arrange
            var fakeUserManager = A.Fake<UserManager<User>>();
            var userCredentialsDto = new Fixture().Create<UserCredentialsDTO>();
            var authenticationService = new AuthenticationService(A.Fake<IMapper>(), fakeUserManager, A.Fake<IJwtGenerator>(), A.Fake<SendingEmail>());
            A.CallTo(() => fakeUserManager.CreateAsync(A<User>.Ignored, A<string>.Ignored))
                .Returns(Task.FromResult(IdentityResult.Failed()));

            // Act
            var result = await authenticationService.SignUpAsync(userCredentialsDto);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ConfirmEmailAsync_ReturnsTrueIfEmailConfirmed()
        {
            // Arrange
            var fakeUserManager = A.Fake<UserManager<User>>();
            var authenticationService = new AuthenticationService(A.Fake<IMapper>(), fakeUserManager, A.Fake<IJwtGenerator>(), A.Fake<SendingEmail>());
            A.CallTo(() => fakeUserManager.FindByIdAsync(A<string>.Ignored))
                .Returns(Task.FromResult(new User()));
            A.CallTo(() => fakeUserManager.ConfirmEmailAsync(A<User>.Ignored, A<string>.Ignored))
                .Returns(Task.FromResult(IdentityResult.Success));

            // Act
            var result = await authenticationService.ConfirmEmailAsync(1, "tokenToken");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ConfirmEmailAsync_ReturnsFalseIfUserNotFound()
        {
            // Arrange
            var fakeUserManager = A.Fake<UserManager<User>>();
            var authenticationService = new AuthenticationService(A.Fake<IMapper>(), fakeUserManager, A.Fake<IJwtGenerator>(), A.Fake<SendingEmail>());
            A.CallTo(() => fakeUserManager.FindByIdAsync(A<string>.Ignored))
                .Returns(Task.FromResult<User>(null));

            // Act
            var result = await authenticationService.ConfirmEmailAsync(1, "tokenToken");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ConfirmEmailAsync_ReturnsFalseIfEmailUnconfirmed()
        {
            // Arrange
            var fakeUserManager = A.Fake<UserManager<User>>();
            var authenticationService = new AuthenticationService(A.Fake<IMapper>(), fakeUserManager, A.Fake<IJwtGenerator>(), A.Fake<SendingEmail>());
            A.CallTo(() => fakeUserManager.FindByIdAsync(A<string>.Ignored))
                .Returns(Task.FromResult<User>(null));
            A.CallTo(() => fakeUserManager.ConfirmEmailAsync(A<User>.Ignored, A<string>.Ignored))
                .Returns(Task.FromResult(IdentityResult.Failed()));

            // Act
            var result = await authenticationService.ConfirmEmailAsync(1, "tokenToken");

            // Assert
            Assert.False(result);
        }
    }
}