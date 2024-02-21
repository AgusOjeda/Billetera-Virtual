using CV.Authentication.Application.Interfaces;
using CV.Authentication.Application.Handlers;
using CV.Authentication.Domain.DTOs.Request;
using CV.Authentication.Domain.DTOs;
using CV.Authentication.Domain.DTOs.Response;
using CV.Authentication.Application.Tools;
using CV.Authentication.Domain.Entities;

namespace CV.Authentication.ApiTest
{
    public class UserHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IEmailService> _mailServiceMock;
        private readonly UserHandler _userHandler;

        public UserHandlerTests()
        {
            _mailServiceMock = new Mock<IEmailService>();
            _userServiceMock = new Mock<IUserService>();
            _userHandler = new UserHandler(_userServiceMock.Object, _mailServiceMock.Object);
        }
        [Fact]
        public async Task Register_UserDoesNotExist_Success()
        {
            // Arrange
            var request = new UserRegisterRequest
            {
                Email = "test@example.com",
                Password = "password"
            };
            var newUserDto = new UserDto
            (
                Id: Guid.NewGuid(),
                Email: request.Email,
                PasswordHash: new byte[] { },
                PasswordSalt: new byte[] { },
                VerifiedAt: DateTime.Now,
                VerificationToken: Tools.CreateCode(),
                PasswordResetToken: null,
                ResetTokenExpires: null,
                State: 1
            );
            var successResponse = new ServerResponse<UserDto>
            {
                Data = newUserDto
            };
            _userServiceMock.Setup(x => x.FindByEmail(request.Email)).ReturnsAsync(false);
            _userServiceMock.Setup(x => x.CreateAsync(request.Email, request.Password)).ReturnsAsync(successResponse);

            // Act
            var result = await _userHandler.Register(request);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Empty(result.Errors);
            Assert.Equal(newUserDto.Id, result.Data.Id);
            Assert.Equal(newUserDto.Email, result.Data.Email);
            _userServiceMock.Verify(x => x.CreateAsync(request.Email, request.Password), Times.Once);
            _userServiceMock.Verify(x => x.FindByEmail(request.Email));
        }
        [Fact]
        public async Task Register_UserAlreadyExists_Failure()
        {
            // Arrange
            var request = new UserRegisterRequest
            {
                Email = "test@example.com",
                Password = "password"
            };
            _userServiceMock.Setup(x => x.FindByEmail(request.Email)).ReturnsAsync(true);

            // Act
            var result = await _userHandler.Register(request);

            // Assert
            Assert.Null(result.Data);
            Assert.NotEmpty(result.Errors);
            _userServiceMock.Verify(x => x.FindByEmail(request.Email), Times.Once);
        }

        [Fact]
        public async Task Login_ShouldReturnErrorMessageUserNotFound_WhenEmailNotExists()
        {
            // Arrange
            var request = new UserLoginRequest
            {
                Email = "test@example.com",
                Password = "password"
            };
            _userServiceMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((UserDto)null);
            // Act
            var result = await _userHandler.Login(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Errors);
            Assert.Null(result.Data);
            Assert.Contains("User not found", result.Errors);
            _userServiceMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Login_ShouldReturnErrorMessageUserNotVerified_WhenVerifiedAtIsNull()
        {
            // Arrange
            var request = new UserLoginRequest
            {
                Email = "test@example.com",
                Password = "password"
            };
            var userDto = new UserDto
               (
                   Id: Guid.NewGuid(),
                   Email: request.Email,
                   PasswordHash: new byte[] { },
                   PasswordSalt: new byte[] { },
                   VerificationToken: Tools.CreateCode(),
                   VerifiedAt: null,
                   PasswordResetToken: null,
                   ResetTokenExpires: null,
                   State: 1
               );
            _userServiceMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(userDto);
            // Act
            var result = await _userHandler.Login(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Errors);
            Assert.Null(result.Data);
            Assert.Contains("User not verified", result.Errors);
            _userServiceMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Login_ShouldReturnErrorMessageInvalidPassword_WhenPasswordIsIncorrect()
        {
            // Arrange
            var request = new UserLoginRequest
            {
                Email = "test@example.com",
                Password = "password"
            };
            var userDto = new UserDto
               (
                   Id: Guid.NewGuid(),
                   Email: request.Email,
                   PasswordHash: new byte[] { },
                   PasswordSalt: new byte[] { },
                   VerificationToken: Tools.CreateCode(),
                   VerifiedAt: DateTime.Now,
                   PasswordResetToken: null,
                   ResetTokenExpires: null,
                   State: 1
               );
            _userServiceMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(userDto);
            // Act
            var result = await _userHandler.Login(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Errors);
            Assert.Null(result.Data);
            Assert.Contains("Invalid password", result.Errors);
            _userServiceMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Login_ShouldReturnUserLoginResponse_WhenLoginIsSuccesfull()
        {
            // Arrange
            var request = new UserLoginRequest
            {
                Email = "test@example.com",
                Password = "TEST_PASSWORD"
            };
            Encrypt.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var userDto = new UserDto
               (
                   Id: Guid.NewGuid(),
                   Email: request.Email,
                   PasswordHash: passwordHash,
                   PasswordSalt: passwordSalt,
                   VerificationToken: Tools.CreateCode(),
                   VerifiedAt: DateTime.Now,
                   PasswordResetToken: null,
                   ResetTokenExpires: null,
            State: 1
               );
            _userServiceMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(userDto);
            _userServiceMock.Setup(x => x.GetToken(request.Email, userDto.Id)).Returns("token");
            // Act
            var result = await _userHandler.Login(request);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            Assert.NotNull(result.Data);
            Assert.Equal(request.Email, result.Data.Email);
            Assert.True(result.Data.Token.Length > 0);
            _userServiceMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _userServiceMock.Verify(x => x.GetToken(request.Email, userDto.Id), Times.Once);
        }

    }
}
