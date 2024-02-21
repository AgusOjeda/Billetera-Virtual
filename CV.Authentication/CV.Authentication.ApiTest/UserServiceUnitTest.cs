using CV.Authentication.AccessData.Interfaces;
using CV.Authentication.Application.Services;
using CV.Authentication.Domain.Common;
using Microsoft.Extensions.Options;
using CV.Authentication.Domain.DTOs.Request;
using CV.Authentication.Domain.DTOs;

namespace CV.Authentication.ApiTest;

public class UserServiceUnitTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserDto _userDto;
    private readonly UserService _userService;
    public UserServiceUnitTest() {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userDto = new UserDto
        (
            Id: Guid.Parse("cb1e3934-35ea-49c0-bda0-7b72d71478fd"),
            Email: "test@test.com",
            PasswordHash: new byte[] { 0x12, 0x34, 0x56 },
            PasswordSalt: new byte[] { 0xAB, 0xCD, 0xEF },
            VerificationToken: "TOKEN",
            VerifiedAt: DateTime.UtcNow,
            PasswordResetToken: null,
            ResetTokenExpires: null,
            State: 1
        );
        var jwtConfig = new JwtConfig();
        var options = Options.Create(jwtConfig);
        _userService = new UserService(_userRepositoryMock.Object, options);
    }

    [Fact]
    public async Task FindByEmail_ShouldReturnTrue_WhenEmailExists()
    {
        //Arrange
        var email = "test@email.com";
        _userRepositoryMock.Setup(repo => repo.FindByEmailAsync(email)).ReturnsAsync(true);

        //Act
        var result = await _userService.FindByEmail(email);

        //Assert
        Assert.True(result);
    }
    [Fact]
    public async Task FindByEmail_ShouldReturnFalse_WhenEmailNoExists()
    {
        //Arrange
        var email = "test@email.com";
        var jwtConfig = new JwtConfig();
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.FindByEmailAsync(email)).ReturnsAsync(false);
        var options = Options.Create(jwtConfig);
        var service = new UserService(mockRepository.Object,options);

        //Act
        var result = await service.FindByEmail(email);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public async Task VerifyEmailToken_ShouldReturnUserDto_WhenTokenIsValid()
    {
        // Arrange
        var request = new VerifyEmailRequest { Token = "TOKEN" };
        var userId = Guid.Parse("cb1e3934-35ea-49c0-bda0-7b72d71478fd");

        _userRepositoryMock.Setup(x => x.GetUserToVerify(userId, request.Token)).ReturnsAsync(_userDto);
        // Act
        var result = await _userService.VerifyEmailToken(request, userId);
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Data);
        Assert.Equal(result.Data.Id, userId);
        _userRepositoryMock.Verify(x => x.GetUserToVerify(userId, request.Token), Times.Once);
    }

    [Fact]
    public async Task VerifyEmailToken_ShouldReturnErrors_WhenTokenIsInvalid()
    {
        // Arrange
        var request = new VerifyEmailRequest { Token = "INVALID_TOKEN" };
        var userId = Guid.NewGuid();

        _userRepositoryMock.Setup(x => x.GetUserToVerify(userId, request.Token)).ReturnsAsync((UserDto)null);
        // Act
        var result = await _userService.VerifyEmailToken(request, userId);
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.Data);
        Assert.Contains("Token no valido", result.Errors);
        _userRepositoryMock.Verify(x => x.GetUserToVerify(userId, request.Token), Times.Once);
    }

    [Fact]
    public async Task ForgotPasswordTokenCreation_ShouldReturnUserDto_WhenEmailIsCorrect()
    {
        //Arrange
        var email = "test@test.com";
        _userRepositoryMock.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(_userDto);
        //Act
        var result = await _userService.ForgotPasswordTokenCreation(email);
        //Assert
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Data);
        Assert.Equal(email, result.Data.Email);
        Assert.Equal(DateTime.UtcNow.AddDays(1).Date, result.Data.ResetTokenExpires?.Date);
        Assert.NotNull(result.Data.PasswordResetToken);
        _userRepositoryMock.Verify(x => x.GetByEmailAsync(email), Times.Once);
    }
    [Fact]
    public async Task ForgotPasswordTokenCreation_ShouldReturnNotFound_WhenEmailIsIncorrect()
    {
        //Arrange
        var email = "test@test.com";
        _userRepositoryMock.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync((UserDto)null);
        //Act
        var result = await _userService.ForgotPasswordTokenCreation(email);
        //Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.Data);
        Assert.Contains("User not found", result.Errors);
        _userRepositoryMock.Verify(x => x.GetByEmailAsync(email), Times.Once);
    }

    [Fact]
    public async Task ResetPassword_ShouldReturnUserNotFoundError_WhenUserNotExists()
    {
        //Arrange
        var request = new ResetPasswordRequest { Password = "NEW_PASSWORD" };
        var userId = Guid.Parse("cb1e3934-35ea-49c0-bda0-7b72d71478fd");
        _userRepositoryMock.Setup(x => x.GetUserById(userId)).ReturnsAsync((UserDto)null);
        //Act
        var result = await _userService.ResetPassword(request, userId);
        //Assert
        Assert.NotEmpty(result.Errors);
        Assert.NotNull(result);
        Assert.Contains("User not found", result.Errors);
        _userRepositoryMock.Verify(x => x.GetUserById(userId), Times.Once);
    }

    [Fact]
    public async Task ResetPassword_ShouldReturnServerResponse_WhenUserExists()
    {
        //Arrange
        var request = new ResetPasswordRequest { Password = "NEW_PASSWORD" };
        var userId = Guid.Parse("cb1e3934-35ea-49c0-bda0-7b72d71478fd");
        _userRepositoryMock.Setup(x => x.GetUserById(userId)).ReturnsAsync(_userDto);
        //Act
        var result = await _userService.ResetPassword(request, userId);
        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsValid);
        _userRepositoryMock.Verify(x => x.GetUserById(userId), Times.Once);
    }


}