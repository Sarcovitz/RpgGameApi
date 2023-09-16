using Microsoft.AspNetCore.Mvc;
using RpgGame.Controllers;
using System.Security.Claims;

namespace RpgGameApiTests.ControllersTests;

[TestFixture]
public class UserControllerTests
{
    private UserController _userController;

    [SetUp]
    public void Setup()
    {
        var userServiceMock = new Mock<IUserService>();

        userServiceMock.Setup(x => x.GetCurrentUserAsync(It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(new UserDTO());

        _userController = new UserController(userServiceMock.Object);
    }

    [Test]
    public async Task MeAsync_Success_ReturnsOk()
    {
        var result = await _userController.MeAsync();
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

}
