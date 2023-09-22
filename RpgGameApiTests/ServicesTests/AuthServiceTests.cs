using Microsoft.Extensions.Options;
using RpgGame.Configuration;
using RpgGame.Models.Entity;
using RpgGame.Models.Request;
using RpgGame.Providers.Interfaces;
using System.Data;
using System.Security.Claims;

namespace RpgGameApiTests.ServicesTests;

[TestFixture]
public class AuthServiceTests
{
    private IAuthService _authService;

    private IOptions<AppConfig> _options;
    private Mock<ICryptographyService> _cryptographyServiceMock;
    private Mock<IEmailService> _emailServiceMock;
    private Mock<ITimeHelper> _timeHelperMock;
    private Mock<IUserRepository> _userRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _options = Options.Create(new AppConfig()
        {
            Secret = "AAAAAAAAAABBBBBBBBBBCCCCCCCCCCDD",
            Url = "test.com:5050",
        });

        _cryptographyServiceMock = new Mock<ICryptographyService>();
        _emailServiceMock = new Mock<IEmailService>();
        _timeHelperMock = new Mock<ITimeHelper>();
        _userRepositoryMock = new Mock<IUserRepository>();

        SetAuthService();
    }

    private void SetAuthService()
    {
        _authService = new AuthService(_options,
            _cryptographyServiceMock.Object,
            _emailServiceMock.Object,
            _timeHelperMock.Object,
            _userRepositoryMock.Object);
    }

    [Test]
    public async Task ConfirmUserAsync_OnSuccess_ShouldReturnSuccessDTO()
    {
        User? userData = new()
        {
            IsConfirmed = false,
        };

        _userRepositoryMock.Setup(x => x.GetByData(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(userData);
        _userRepositoryMock.Setup(x => x.UpdateAsync(userData))
            .ReturnsAsync(1);

        SetAuthService();

        ulong id = 123;
        string email = "mail@test.com";
        string username = "username";

        var result = await _authService.ConfirmUserAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<SuccessDTO>());
        Assert.That(result.IsSuccess, Is.True);
    }
    
    [Test]
    public void ConfirmUserAsync_UserIsNull_ShouldThrowKeyNotFoundException()
    {
        User? userData = null;

        _userRepositoryMock.Setup(x => x.GetByData(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(userData);

        SetAuthService();

        ulong id = 123;
        string email = "mail@test.com";
        string username = "username";

        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
        {
            await _authService.ConfirmUserAsync(id, email, username);
        });

        Assert.That(exception.Message, Is.EqualTo("There is no user matching supplied data."));
    }
    
    [Test]
    public void ConfirmUserAsync_UserIsConfirmed_ShouldThrowKeyNotFoundExceptionAndValidMessage()
    {
        User? userData = new()
        {
            IsConfirmed = true,
        };

        _userRepositoryMock.Setup(x => x.GetByData(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(userData);

        SetAuthService();

        ulong id = 123;
        string email = "mail@test.com";
        string username = "username";

        var exception = Assert.ThrowsAsync<ArgumentException>(async () => 
        {
            await _authService.ConfirmUserAsync(id, email, username);
        });

        Assert.That(exception.Message, Is.EqualTo("User is already confirmed"));
    }

    [Test]
    public void ConfirmUserAsync_WhenUserUpdateFails_ShouldThrowException()
    {
        User? userData = new()
        {
            IsConfirmed = false,
        };

        _userRepositoryMock.Setup(x => x.GetByData(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(userData);
        _userRepositoryMock.Setup(x => x.UpdateAsync(userData))
            .ReturnsAsync(0);

        SetAuthService();

        ulong id = 123;
        string email = "mail@test.com";
        string username = "username";

        var exception = Assert.ThrowsAsync<Exception>(async () =>
        {
            await _authService.ConfirmUserAsync(id, email, username);
        });

        Assert.That(exception.Message, Is.EqualTo("Can't confirm user, please contact with support."));
    }

    [Test]
    public void GenerateToken_ReturnsTokenDTO()
    {
        _timeHelperMock.Setup(x => x.GetCurrentUtcTime())
            .Returns(new DateTime(2030, 1, 1, 0, 0, 0));

        SetAuthService();

        User user = new()
        {
            Username = "Username",
            Id = 123,
            Email = "test@test.pl",
            IsConfirmed = true,
            Role = UserRole.Player
        };

        TokenDTO expected = new()
        {
            Username = "Username",
            AccessTokenExpirationDate = 123,
            IsConfirmed = true,
            Role = 0,
            UserId = 123,
        };

        var result = _authService.GenerateToken(user);
        Assert.Multiple(() =>
        {
            Assert.That(result.AccessTokenExpirationDate, Is.EqualTo(_timeHelperMock.Object.GetUnixTimestampFromDate(new DateTime(2030,1,1,12,0,0))));
            Assert.That(result.Username, Is.EqualTo(expected.Username));
            Assert.That(result.UserId, Is.EqualTo(expected.UserId));
            Assert.That(result.IsConfirmed, Is.EqualTo(expected.IsConfirmed));
            Assert.That(result.Role, Is.EqualTo(expected.Role));
        });
    }

    [Test]
    public async Task IsUserConfirmedAsync_OnSuccessWhenConfirmedUser_ReturnsIsConfirmedUserDTO()
    {
        User? userData = new()
        {
            IsConfirmed = true,
        };

        _userRepositoryMock.Setup(x => x.GetByData(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(userData);

        SetAuthService();

        ulong id = 123;
        string email = "mail@test.com";
        string username = "username";

        var result = await _authService.IsUserConfirmedAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<IsUserConfirmedDTO>());
        Assert.That(result.IsConfirmed, Is.True);
    }

    [Test]
    public async Task IsUserConfirmedAsync_OnSuccessWhenUnconfirmedUser_ReturnsIsConfirmedUserDTO()
    {
        User? userData = new()
        {
            IsConfirmed = false,
        };

        _userRepositoryMock.Setup(x => x.GetByData(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(userData);

        SetAuthService();

        ulong id = 123;
        string email = "mail@test.com";
        string username = "username";

        var result = await _authService.IsUserConfirmedAsync(id, email, username);
        Assert.That(result, Is.InstanceOf<IsUserConfirmedDTO>());
        Assert.That(result.IsConfirmed, Is.False);
    }
    
    [Test]
    public void IsUserConfirmedAsync_WhenUserNotFound_ShouldThrowKeyNotFoundException()
    {
        User? userData = null;

        _userRepositoryMock.Setup(x => x.GetByData(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(userData);

        SetAuthService();

        ulong id = 123;
        string email = "mail@test.com";
        string username = "username";

        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
        {
            await _authService.IsUserConfirmedAsync(id, email, username);
        });

        Assert.That(exception.Message, Is.EqualTo("There is no user matching supplied data."));
    }

    [Test]
    public async Task LoginAsync_OnSuccess_ShouldReturnTokenDTO()
    {
        User user = new()
        {
            Username = "Username",
            Id = 123,
            Email = "test@test.pl",
            IsConfirmed = true,
            Role = UserRole.Player,
            Password = "Password"
        };

        _userRepositoryMock.Setup(x => x.GetByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        _cryptographyServiceMock.Setup(x => x.Sha256Hash(It.IsAny<string>()))
            .Returns(user.Password);

        _timeHelperMock.Setup(x => x.GetCurrentUtcTime())
            .Returns(new DateTime(2030,1,1,0,0,0));

        SetAuthService();

        LoginUserRequest loginRequest = new()
        {
            Password = "Password",
            Username = "Username"
        };

        var result = await _authService.LoginAsync(loginRequest);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<TokenDTO>());
        });
    }

    [Test]
    public void LoginAsync_WhenUserIsNull_ShouldThrowKeyNotFoundException()
    {
        User? user = null;

        _userRepositoryMock.Setup(x => x.GetByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        SetAuthService();

        LoginUserRequest loginRequest = new()
        {
            Password = "Password",
            Username = "Username"
        };

        string expectedMessage = "There is no user with supplied username";

        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
        {
            await _authService.LoginAsync(loginRequest);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public void LoginAsync_WhenHashNotEqualsPassword_ShouldThrowArgumentException()
    {
        User user = new()
        {
            Username = "Username",
            Id = 123,
            Email = "test@test.pl",
            IsConfirmed = true,
            Role = UserRole.Player,
            Password = "Password"
        };

        string returnedPasswordHash = "Different Value";

        _userRepositoryMock.Setup(x => x.GetByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        _cryptographyServiceMock.Setup(x => x.Sha256Hash(It.IsAny<string>()))
            .Returns(returnedPasswordHash);

        SetAuthService();

        LoginUserRequest loginRequest = new()
        {
            Password = "Password",
            Username = "Username"
        };

        string expectedMessage = "Supplied password is wrong.";

        var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _authService.LoginAsync(loginRequest);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public void LoginAsync_WhenUserIsUnconfirmed_ShouldThrowArgumentException()
    {
        User user = new()
        {
            Username = "Username",
            Id = 123,
            Email = "test@test.pl",
            IsConfirmed = false,
            Role = UserRole.Player,
            Password = "Password"
        };

        string returnedPasswordHash = "Password";

        _cryptographyServiceMock.Setup(x => x.Sha256Hash(It.IsAny<string>()))
            .Returns(returnedPasswordHash);

        _emailServiceMock.Setup(x => x.SendAccountConfirmationEmail(It.IsAny<User>()))
            .Returns(true);

        _userRepositoryMock.Setup(x => x.GetByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        SetAuthService();

        LoginUserRequest loginRequest = new()
        {
            Password = "Password",
            Username = "Username"
        };

        string expectedMessage = $"This account is not confirmed, confirmation link has been sent to: {user.Email}";

        var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _authService.LoginAsync(loginRequest);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public void LoginAsync_WhenUserIsUnconfirmedAndEmailSendingFails_ShouldThrowException()
    {
        User user = new()
        {
            Username = "Username",
            Id = 123,
            Email = "test@test.pl",
            IsConfirmed = false,
            Role = UserRole.Player,
            Password = "Password"
        };

        string returnedPasswordHash = "Password";

        _cryptographyServiceMock.Setup(x => x.Sha256Hash(It.IsAny<string>()))
            .Returns(returnedPasswordHash);

        _emailServiceMock.Setup(x => x.SendAccountConfirmationEmail(It.IsAny<User>()))
            .Returns(false);

        _userRepositoryMock.Setup(x => x.GetByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        SetAuthService();

        LoginUserRequest loginRequest = new()
        {
            Password = "Password",
            Username = "Username"
        };

        string expectedMessage = "This account is not confirmed, confirmation link could not be sent, please contact support.";

        var exception = Assert.ThrowsAsync<Exception>(async () =>
        {
            await _authService.LoginAsync(loginRequest);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task RegisterAsync_OnSuccess_ShouldReturnRegisterUserRequest()
    {
        RegisterUserRequest registerRequest = new()
        {
            Email = "test@test.com",
            Username = "Username",
            Password = "Password",
            Password2 = "Password",
        };

        User? nullUser = null;

        User? newUser = new()
        {
            Id = 123,
            Username = "Username",
            Email = "test@test.com",
        };

        _emailServiceMock.Setup(x => x.SendAccountConfirmationEmail(It.IsAny<User>()))
            .Returns(true);

        _userRepositoryMock.Setup(x => x.GetByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync(nullUser);
        _userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(nullUser);
        _userRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<User>()))
            .ReturnsAsync(newUser);

        SetAuthService();

        var result = await _authService.RegisterAsync(registerRequest);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<RegisterUserDTO>());
            Assert.That(result.Id, Is.EqualTo(newUser.Id));
            Assert.That(result.Username, Is.EqualTo(newUser.Username));
            Assert.That(result.Email, Is.EqualTo(newUser.Email));
        });
    }
    
    [Test]
    public void RegisterAsync_WhenPasswordsAreDifferent_ShouldThrowArgumentException()
    {
        RegisterUserRequest registerRequest = new()
        {
            Email = "test@test.com",
            Username = "Username",
            Password = "AAAAAAAAAAAA",
            Password2 = "BBBBBBBBBBB",
        };

        string expectedMessage = "Supplied paswords are not equal.";

        var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _authService.RegisterAsync(registerRequest);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public void RegisterAsync_WhenGetByUsernameReturnsUser_ShouldThrowDuplicateNameException()
    {
        RegisterUserRequest registerRequest = new()
        {
            Email = "test@test.com",
            Username = "Username",
            Password = "Password",
            Password2 = "Password",
        };

        User? foundUser = new()
        {
            Id = 123,
            Username = "Username",
            Email = "test@test.com",
        };

        _userRepositoryMock.Setup(x => x.GetByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync(foundUser);

        SetAuthService();

        string expectedMessage = "User with supplied username already exists.";

        var exception = Assert.ThrowsAsync<DuplicateNameException>(async () =>
        {
            await _authService.RegisterAsync(registerRequest);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public void RegisterAsync_WhenGetByEmailReturnsUser_ShouldThrowDuplicateNameException()
    {
        RegisterUserRequest registerRequest = new()
        {
            Email = "test@test.com",
            Username = "Username",
            Password = "Password",
            Password2 = "Password",
        };

        User? nullUser = null;

        User? foundUser = new()
        {
            Id = 123,
            Username = "Username",
            Email = "test@test.com",
        };

        _userRepositoryMock.Setup(x => x.GetByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync(nullUser);
        _userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(foundUser);

        SetAuthService();

        string expectedMessage = "User with supplied e-mail already exists.";

        var exception = Assert.ThrowsAsync<DuplicateNameException>(async () =>
        {
            await _authService.RegisterAsync(registerRequest);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public void RegisterAsync_WhenEmailSendoingFails_ShouldThrowException()
    {
        RegisterUserRequest registerRequest = new()
        {
            Email = "test@test.com",
            Username = "Username",
            Password = "Password",
            Password2 = "Password",
        };

        User? nullUser = null;

        _emailServiceMock.Setup(x => x.SendAccountConfirmationEmail(It.IsAny<User>()))
            .Returns(false);

        _userRepositoryMock.Setup(x => x.GetByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync(nullUser);
        _userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(nullUser);

        SetAuthService();

        string expectedMessage = "Account has been created but confirmation link could not be sent, please contact support.";

        var exception = Assert.ThrowsAsync<Exception>(async () =>
        {
            await _authService.RegisterAsync(registerRequest);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task RenewTokenAsync_OnSuccess_ReturnsTokenDTO()
    {
        var userId = 12345UL;
        var username = "testuser";
        var expiringTime = new DateTimeOffset(new DateTime(2030, 1, 1, 0, 0, 0))
            .ToUnixTimeSeconds();

        var claims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("Name", username),
            new Claim("Id", userId.ToString()),
            new Claim("Exp", expiringTime.ToString())
        }, "test"));

        var renewTokenRequest = new RenewTokenRequest
        {
            UserId = userId,
            Username = username
        };

        User? user = new();

        _timeHelperMock.Setup(x => x.GetCurrentUnixTimestamp())
            .Returns(expiringTime - 1200);
        _timeHelperMock.Setup(x => x.GetCurrentUtcTime())
            .Returns(new DateTime(2030, 1, 1, 0, 0, 0));

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId))
            .ReturnsAsync(user);

        SetAuthService();

        var result = await _authService.RenewTokenAsync(claims, renewTokenRequest);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<TokenDTO>());
    }
    
    [Test]
    public void RenewTokenAsync_WhenTokenExpiredForTooLong_ThrowsArgumentException()
    {
        var userId = 12345UL;
        var username = "testuser";
        var expiringTime = new DateTimeOffset(new DateTime(2030, 1, 1, 0, 0, 0))
            .ToUnixTimeSeconds();

        var claims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("Name", username),
            new Claim("Id", userId.ToString()),
            new Claim("Exp", expiringTime.ToString())
        }, "test"));

        var renewTokenRequest = new RenewTokenRequest
        {
            UserId = userId,
            Username = username
        };

        User? user = new();

        _timeHelperMock.Setup(x => x.GetCurrentUnixTimestamp())
            .Returns(expiringTime + 12_000);
        _timeHelperMock.Setup(x => x.GetCurrentUtcTime())
            .Returns(new DateTime(2030, 1, 1, 0, 0, 0));

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId))
            .ReturnsAsync(user);

        SetAuthService();

        string expectedMessage = "Token is expired for too long.";

        var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _authService.RenewTokenAsync(claims, renewTokenRequest);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public void RenewTokenAsync_WhenUserIdNotMatchClaimsId_ThrowsArgumentException()
    {
        var userId = 12345UL;
        var username = "testuser";
        var expiringTime = new DateTimeOffset(new DateTime(2030, 1, 1, 0, 0, 0))
            .ToUnixTimeSeconds();

        var claims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("Name", username),
            new Claim("Id", "321"),
            new Claim("Exp", expiringTime.ToString())
        }, "test"));

        var renewTokenRequest = new RenewTokenRequest
        {
            UserId = userId,
            Username = username
        };

        User? user = new();

        _timeHelperMock.Setup(x => x.GetCurrentUnixTimestamp())
            .Returns(expiringTime - 1200);
        _timeHelperMock.Setup(x => x.GetCurrentUtcTime())
            .Returns(new DateTime(2030, 1, 1, 0, 0, 0));

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId))
            .ReturnsAsync(user);

        SetAuthService();

        string expectedMessage = "User data does not match data provided with token.";

        var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _authService.RenewTokenAsync(claims, renewTokenRequest);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public void RenewTokenAsync_WhenUsernameNotMatchClaimsUsername_ThrowsArgumentException()
    {
        var userId = 12345UL;
        var username = "testuser";
        var expiringTime = new DateTimeOffset(new DateTime(2030, 1, 1, 0, 0, 0))
            .ToUnixTimeSeconds();

        var claims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("Name", "wrongUsername"),
            new Claim("Id", userId.ToString()),
            new Claim("Exp", expiringTime.ToString())
        }, "test"));

        var renewTokenRequest = new RenewTokenRequest
        {
            UserId = userId,
            Username = username
        };

        User? user = new();

        _timeHelperMock.Setup(x => x.GetCurrentUnixTimestamp())
            .Returns(expiringTime - 1200);
        _timeHelperMock.Setup(x => x.GetCurrentUtcTime())
            .Returns(new DateTime(2030, 1, 1, 0, 0, 0));

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId))
            .ReturnsAsync(user);

        SetAuthService();

        string expectedMessage = "User data does not match data provided with token.";

        var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _authService.RenewTokenAsync(claims, renewTokenRequest);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }

    [Test]
    public void RenewTokenAsync_WhenCantFindUserFromToken_ThrowsKeyNotFoundException()
    {
        var userId = 12345UL;
        var username = "testuser";
        var expiringTime = new DateTimeOffset(new DateTime(2030, 1, 1, 0, 0, 0))
            .ToUnixTimeSeconds();

        var claims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("Name", username),
            new Claim("Id", userId.ToString()),
            new Claim("Exp", expiringTime.ToString())
        }, "test"));

        var renewTokenRequest = new RenewTokenRequest
        {
            UserId = userId,
            Username = username
        };

        User? user = null;

        _timeHelperMock.Setup(x => x.GetCurrentUnixTimestamp())
            .Returns(expiringTime - 1_200);
        _timeHelperMock.Setup(x => x.GetCurrentUtcTime())
            .Returns(new DateTime(2030, 1, 1, 0, 0, 0));

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId))
            .ReturnsAsync(user);

        SetAuthService();

        string expectedMessage = "There is no user mathcing supplied data.";

        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
        {
            await _authService.RenewTokenAsync(claims, renewTokenRequest);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task ResendConfirmationMailAsync_OnSuccess_ReturnsSuccessDTO()
    {
        ulong id = 123;
        string email = "test@test.pl";
        string username = "username";

        User? user = new();

        _emailServiceMock.Setup(x => x.SendAccountConfirmationEmail(It.IsAny<User>()))
            .Returns(true);

        _userRepositoryMock.Setup(x => x.GetByData(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(user);

        SetAuthService();

        var result = await _authService.ResendConfirmationMailAsync(id, email, username);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<SuccessDTO>());
        Assert.That(result.IsSuccess, Is.True);
    }
    
    [Test]
    public void ResendConfirmationMailAsync_WhenUserIsNotFound_ThrowsKeyNotFoundException()
    {
        ulong id = 123;
        string email = "test@test.pl";
        string username = "username";

        User? user = null;

        _userRepositoryMock.Setup(x => x.GetByData(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(user);

        SetAuthService();

        string expectedMessage = "There is no user matching supplied data.";

        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
        {
            await _authService.ResendConfirmationMailAsync(id, email, username);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public void ResendConfirmationMailAsync_WhenUserIsAlreadyConfirmed_ThrowsArgumentException()
    {
        ulong id = 123;
        string email = "test@test.pl";
        string username = "username";

        User? user = new()
        {
            IsConfirmed = true,
        };

        _userRepositoryMock.Setup(x => x.GetByData(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(user);

        SetAuthService();

        string expectedMessage = "User is already confirmed";

        var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _authService.ResendConfirmationMailAsync(id, email, username);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public void ResendConfirmationMailAsync_WhenEmailSendingFails_ThrowsException()
    {
        ulong id = 123;
        string email = "test@test.pl";
        string username = "username";

        User? user = new()
        {
            IsConfirmed = false,
        };

        _emailServiceMock.Setup(x => x.SendAccountConfirmationEmail(It.IsAny<User>()))
            .Returns(false);

        _userRepositoryMock.Setup(x => x.GetByData(It.IsAny<ulong>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(user);

        SetAuthService();

        string expectedMessage = "Confirmation link could not be sent, please contact support.";

        var exception = Assert.ThrowsAsync<Exception>(async () =>
        {
            await _authService.ResendConfirmationMailAsync(id, email, username);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
}
