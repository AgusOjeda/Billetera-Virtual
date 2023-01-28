using System.Threading.Tasks;
using CV.Authentication.AccessData.Interfaces;
using Moq;
using Xunit;
using CV.Authentication.Application.Services;
using CV.Authentication.Domain.Common;
using CV.Authentication.AccessData.Repositories;
using Microsoft.Extensions.Options;

namespace CV.Authentication.ApiTest;

public class UserServiceUnitTest
{
    [Fact]
    public async void FindByEmail_ShouldReturnTrue_WhenEmailExists()
    {
        //Arrange
        var email = "test@email.com";
        var jwtConfig = new JwtConfig();
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.FindByEmailAsync(email)).ReturnsAsync(true);
        var options = Options.Create(jwtConfig);
        var service = new UserService(mockRepository.Object,options);

        //Act
        var result = await service.FindByEmail(email);

        //Assert
        Assert.True(result);
    }
    [Fact]
    public async void FindByEmail_ShouldReturnFalse_WhenEmailNoExists()
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
}