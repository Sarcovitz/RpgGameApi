using Microsoft.Extensions.Options;
using RpgGame.Configuration;
using RpgGame.Models.Entity;
using RpgGame.Models.Request;
using RpgGame.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RpgGameApiTests.ServicesTests;

[TestFixture]
public class UserServiceTests
{
    private IUserService _userService;

    private Mock<IUserRepository> _userRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _userRepositoryMock = new Mock<IUserRepository>();

        SetUserService();
    }

    private void SetUserService()
    {
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Test]
    public async Task GetCurrentUserAsync_OnSuccess_ShouldReturnUserDTO()
    {
        ulong userId = 123;

        var claims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("Id", userId.ToString()),
        }, "test"));

        User? user = new()
        {
            Username = "Username",
            Email = "test@test.pl",
            CharacterSlots = 3,
            CreationDate = 1234567890,
            IsConfirmed = true,
            LastLoginDate = 1234567890,
            Role = UserRole.Player,
        };

        UserDTO expected = new()
        {
            CharacterSlots = 3,
            CreationDate = 1234567890,
            Email = "test@test.pl",
            IsConfirmed = true,
            LastLoginDate = 1234567890,
            Role = UserRole.Player,
            Username = "Username",
        };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<ulong>()))
            .ReturnsAsync(user);

        SetUserService();

        var result = await _userService.GetCurrentUserAsync(claims);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<UserDTO>());
        Assert.Multiple(() =>
        {
            Assert.That(result.CharacterSlots, Is.EqualTo(expected.CharacterSlots));
            Assert.That(result.CreationDate, Is.EqualTo(expected.CreationDate));
            Assert.That(result.Email, Is.EqualTo(expected.Email));
            Assert.That(result.IsConfirmed, Is.EqualTo(expected.IsConfirmed));
            Assert.That(result.LastLoginDate, Is.EqualTo(expected.LastLoginDate));
            Assert.That(result.Role, Is.EqualTo(expected.Role));
            Assert.That(result.Username, Is.EqualTo(expected.Username));
        });
    }
    
    [Test]
    public async Task GetCurrentUserAsync_WhenUserNotFound_ShouldThrowKeyNotFoundException()
    {
        ulong userId = 123;

        var claims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("Id", userId.ToString()),
        }, "test"));

        User? user = null;

        _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<ulong>()))
            .ReturnsAsync(user);

        SetUserService();

        string expectedMessage = "Provided token does not match to any user.";

        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
        {
            await _userService.GetCurrentUserAsync(claims);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
}
