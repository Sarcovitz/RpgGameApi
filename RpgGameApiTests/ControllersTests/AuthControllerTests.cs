using Microsoft.AspNetCore.Mvc;
using Moq;
using RpgGame.Misc;
using System.Security.Claims;

namespace RpgGameApiTests.ControllersTests;

[TestFixture]
public  class AuthControllerTests
{
    private AuthController _authController;

    [SetUp]
    public void Setup()
    {
        var authServiceMock = new Mock<IAuthService>();
        authServiceMock.Setup(x => x.ConfirmUserAsync(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new SuccessDTO());
        authServiceMock.Setup(x => x.IsUserConfirmedAsync(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new IsUserConfirmedDTO());
        authServiceMock.Setup(x => x.LoginAsync(It.IsAny<LoginUserRequest>()))
            .ReturnsAsync(new TokenDTO());
        authServiceMock.Setup(x => x.RegisterAsync(It.IsAny<RegisterUserRequest>()))
            .ReturnsAsync(new RegisterUserDTO());
        authServiceMock.Setup(x => x.RenewTokenAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<RenewTokenRequest>()))
            .ReturnsAsync(new TokenDTO());
        authServiceMock.Setup(x => x.ResendConfirmationMailAsync(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new SuccessDTO());

        _authController = new AuthController(authServiceMock.Object);
    }

    [Test]
    public async Task ConfirmUserAsync_ValidDataAndSuccess_ReturnsOk()
    {
        ulong? id = 123;
        string? email = "email@test.com";
        string? username = "Username";

        var result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task ConfirmUserAsync_AllValuesAreNull_ReturnsBadRequest()
    {
        ulong? id = null;
        string? email = null;
        string? username = null;

        var result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task ConfirmUserAsync_IdIsNullOrEmpty_ReturnsBadRequest()
    {
        ulong? id = null;
        string? email = "email@test.com";
        string? username = "Username";

        var result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

        id = 0;

        result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task ConfirmUserAsync_IdIsNullOrEmpty_BadRequestMessage()
    {
        ulong? id = null;
        string? email = "email@test.com";
        string? username = "Username";

        string expectedMessage = "Id cannot be empty or null.";

        var result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));

        id = 0;

        result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task ConfirmUserAsync_EmailIsNullOrEmpty_ReturnsBadRequest()
    {
        ulong? id = 123;
        string? email = null;
        string? username = "Username";

        var result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

        email = string.Empty;

        result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task ConfirmUserAsync_EmailIsNullOrEmpty_BadRequestMessage()
    {
        ulong? id = 123;
        string? email = null;
        string? username = "Username";

        string expectedMessage = "Email cannot be empty or null.";

        var result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));

        email = string.Empty;

        result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task ConfirmUserAsync_UsernameIsNullOrEmpty_ReturnsBadRequest()
    {
        ulong? id = 123;
        string? email = "email@test.com";
        string? username = null;

        var result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

        username = string.Empty;

        result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task ConfirmUserAsync_UsernameNullOrEmpty_BadRequestMessage()
    {
        ulong? id = 123;
        string? email = "email@test.com";
        string? username = null;

        string expectedMessage = "Username cannot be empty or null.";

        var result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));

        result = await _authController.ConfirmUserAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task IsConfirmedUserAsync_ValidDataAndSuccess_ReturnsOk()
    {
        ulong? id = 123;
        string? email = "email@test.com";
        string? username = "Username";

        var result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task IsConfirmedUserAsync_AllValuesAreNull_ReturnsBadRequest()
    {
        ulong? id = null;
        string? email = null;
        string? username = null;

        var result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task IsConfirmedUserAsync_IdIsNullOrZero_ReturnsBadRequest()
    {
        ulong? id = null;
        string? email = "email@test.com";
        string? username = "Username";

        var result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

        id = 0;

        result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task IsConfirmedUserAsync_IdIsNullOrEmpty_BadRequestMessage()
    {
        ulong? id = null;
        string? email = "email@test.com";
        string? username = "Username";

        string expectedMessage = "Id cannot be empty or null.";

        var result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));

        id = 0;

        result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task IsConfirmedUserAsync_EmailIsNullOrEmpty_ReturnsBadRequest()
    {
        ulong? id = 123;
        string? email = null;
        string? username = "Username";

        var result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

        email = string.Empty;
        result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }
    
    [Test]
    public async Task IsConfirmedUserAsync_EmailIsNullOrEmpty_BadRequestMessage()
    {
        ulong? id = 123;
        string? email = null;
        string? username = "Username";

        string expectedMessage = "Email cannot be empty or null.";

        var result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));

        email = string.Empty;

        result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task IsConfirmedUserAsync_UsernameIsNullOrEmpty_ReturnsBadRequest()
    {
        ulong? id = 123;
        string? email = "email@test.com";
        string? username = null;

        var result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

        username = string.Empty;

        result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task IsConfirmedUserAsync_UsernameIsNullOrEmpty_BadRequestMessage()
    {
        ulong? id = 123;
        string? email = "email@test.com";
        string? username = null;

        string expectedMessage = "Username cannot be empty or null.";

        var result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));

        username = string.Empty;

        result = await _authController.IsConfirmedUserAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task LoginAsync_LoginSuccess_ReturnsOk()
    {
        LoginUserRequest? model = new()
        {
            Username = "Username",
            Password = "Password"
        };

        var result = await _authController.LoginAsync(model);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task LoginAsync_ModelIsNull_ReturnsBadRequest()
    {
        LoginUserRequest? model = null;

        var result = await _authController.LoginAsync(model);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task LoginAsync_ModelIsNull_BadRequestMessage()
    {
        LoginUserRequest? model = null;

        string expectedMessage = "Model cannot be null.";

        var result = await _authController.LoginAsync(model);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public async Task LoginAsync_ModelStateHasErrors_ReturnsBadRequest()
    {
        _authController.ModelState.AddModelError("Error", "Error Message");

        LoginUserRequest? model = new();

        var result = await _authController.LoginAsync(model);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task LoginAsync_ModelStateHasErrors_BadRequestMessage()
    {
        _authController.ModelState.AddModelError("Error", "Error Message");

        LoginUserRequest? model = new();

        string expectedMessage = "Error Message";

        var result = await _authController.LoginAsync(model);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task RegisterAsync_RegisterSuccess_ReturnsCreated()
    {
        RegisterUserRequest? model = new()
        {
            Email = "email@domain.pl",
            Username = "Username",
            Password = "Password",
            Password2 = "Password"
        };

        var result = await _authController.RegisterAsync(model);
        Assert.That(result, Is.InstanceOf<CreatedAtRouteResult>());
    }

    [Test]
    public async Task RegisterAsync_RegisterSuccess_ReturnsCreatedUri()
    {
        RegisterUserRequest? model = new()
        {
            Email = "email@domain.pl",
            Username = "Username",
            Password = "Password",
            Password2 = "Password"
        };

        var result = await _authController.RegisterAsync(model);
        Assert.That((result as CreatedAtRouteResult)?.RouteName, Is.EqualTo("/user/me"));
    }

    [Test]
    public async Task RegisterAsync_ModelIsNull_ReturnsBadRequest()
    {
        RegisterUserRequest? model = null;

        var result = await _authController.RegisterAsync(model);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task RegisterAsync_ModelIsNull_BadRequestMessage()
    {
        RegisterUserRequest? model = null;

        string expectedMessage = "Model cannot be null.";

        var result = await _authController.RegisterAsync(model);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task RegisterAsync_ModelStateHasErrors_ReturnsBadRequest()
    {
        _authController.ModelState.AddModelError("Error", "Error Message");

        RegisterUserRequest? model = new();

        var result = await _authController.RegisterAsync(model);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task RegisterAsync_ModelStateHasErrors_BadRequestMessage()
    {
        _authController.ModelState.AddModelError("Error", "Error Message");

        RegisterUserRequest? model = new();

        string expectedMessage = "Error Message";

        var result = await _authController.RegisterAsync(model);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task RenewTokenAsync_OnSuccess_ReturnsOk()
    {
        RenewTokenRequest? model = new()
        {
            UserId = 1,
            Username = "Username"
        };

        var result = await _authController.RenewTokenAsync(model);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task RenewTokenAsync_ModelIsNull_ReturnsBadRequest()
    {
        RenewTokenRequest? model = null;

        var result = await _authController.RenewTokenAsync(model);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task RenewTokenAsync_ModelIsNull_BadRequestMessage()
    {
        RenewTokenRequest? model = null;

        string expectedMessage = "Model cannot be null.";

        var result = await _authController.RenewTokenAsync(model);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task RenewTokenAsync_ModelStateHasErrors_ReturnsBadRequest()
    {
        _authController.ModelState.AddModelError("Error", "Error Message");

        RenewTokenRequest? model = new();

        var result = await _authController.RenewTokenAsync(model);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task RenewTokenAsync_ModelStateHasErrors_BadRequestMessage()
    {
        _authController.ModelState.AddModelError("Error", "Error Message");

        RenewTokenRequest? model = new();

        string expectedMessage = "Error Message";

        var result = await _authController.RenewTokenAsync(model);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task ResendConfirmationMailAsync_OnSuccess_ReturnsOk()
    {
        ulong? id = 123;
        string? email = "email@test.com";
        string? username = "Username";

        var result = await _authController.ResendConfirmationMailAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task ResendConfirmationMailAsync_IdIsNullOrZero_ReturnsBadRequest()
    {
        ulong? id = null;
        string? email = "email@test.com";
        string? username = "Username";

        var result = await _authController.ResendConfirmationMailAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

        id = 0;

        result = await _authController.ResendConfirmationMailAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task ResendConfirmationMailAsync_IdIsNullOrEmpty_BadRequestMessage()
    {
        ulong? id = null;
        string? email = "email@test.com";
        string? username = "Username";

        string expectedMessage = "Id cannot be empty or null.";

        var result = await _authController.ResendConfirmationMailAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));

        id = 0;

        result = await _authController.ResendConfirmationMailAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task ResendConfirmationMailAsync_EmailIsNullOrEmpty_ReturnsBadRequest()
    {
        ulong? id = 123;
        string? email = null;
        string? username = "Username";

        var result = await _authController.ResendConfirmationMailAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

        email = string.Empty;

        result = await _authController.ResendConfirmationMailAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task ResendConfirmationMailAsync_EmailIsNullOrEmpty_BadRequestMessage()
    {
        ulong? id = 123;
        string? email = null;
        string? username = "Username";

        string expectedMessage = "Email cannot be empty or null.";

        var result = await _authController.ResendConfirmationMailAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));

        email = string.Empty;

        result = await _authController.ResendConfirmationMailAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task ResendConfirmationMailAsync_UsernameIsNullOrEmpty_ReturnsBadRequest()
    {
        ulong? id = 123;
        string? email = "email@test.com";
        string? username = null;

        var result = await _authController.ResendConfirmationMailAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

        username = string.Empty;

        result = await _authController.ResendConfirmationMailAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task ResendConfirmationMailAsync_UsernameNullOrEmpty_BadRequestMessage()
    {
        ulong? id = 123;
        string? email = "email@test.com";
        string? username = null;

        var result = await _authController.ResendConfirmationMailAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo("Username cannot be empty or null."));

        username = string.Empty;

        result = await _authController.ResendConfirmationMailAsync(id, email, username);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo("Username cannot be empty or null."));
    }
}

