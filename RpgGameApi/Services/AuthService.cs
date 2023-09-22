using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RpgGame.Configuration;
using RpgGame.Models.DTO;
using RpgGame.Models.Entity;
using RpgGame.Models.Request;
using RpgGame.Providers.Interfaces;
using RpgGame.Repositories.Interfaces;
using RpgGame.Services.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RpgGame.Services;

public class AuthService : IAuthService
{
    private readonly AppConfig _appConfig;
    private readonly ICryptographyService _cryptographyService;
    private readonly IEmailService _emailService;
    private readonly ITimeHelper _timeHelper;
    private readonly IUserRepository _userRepository;

    public AuthService(IOptions<AppConfig> appConfig,
        ICryptographyService cryptographyService, 
        IEmailService emailService, 
        ITimeHelper timeProvider,
        IUserRepository userRepository)
    {
        _appConfig = appConfig.Value;
        _cryptographyService = cryptographyService;
        _emailService = emailService;
        _timeHelper = timeProvider;
        _userRepository = userRepository;
    }

    public async  Task<SuccessDTO> ConfirmUserAsync(ulong id, string email, string username)
    {
        User? user = await _userRepository.GetByData(id, email, username);
        if (user is null)
            throw new KeyNotFoundException("There is no user matching supplied data.");

        if(user.IsConfirmed) 
            throw new ArgumentException("User is already confirmed");

        user.IsConfirmed = true;

        int result = await _userRepository.UpdateAsync(user);
        if(result != 1) 
            throw new Exception("Can't confirm user, please contact with support.");

        return new SuccessDTO()
        {
            IsSuccess = true,
        };
    }

    public TokenDTO GenerateToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_appConfig.Secret);
        SymmetricSecurityKey symetricKey = new(key);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Name", user.Username.ToString()),
                new Claim("Email", user.Email.ToString()),
                new Claim("Id", user.Id.ToString())
            }),
            Expires = _timeHelper.GetCurrentUtcTime().AddHours(12),
            SigningCredentials = new SigningCredentials(symetricKey, SecurityAlgorithms.HmacSha256Signature),
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken accessToken = tokenHandler.CreateToken(tokenDescriptor);

        return new TokenDTO()
        {
            UserId = user.Id,
            Username = user.Username,
            AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            AccessTokenExpirationDate = _timeHelper.GetUnixTimestampFromDate(accessToken.ValidTo),
            IsConfirmed = user.IsConfirmed
        };
    }

    public async Task<IsUserConfirmedDTO> IsUserConfirmedAsync(ulong id, string email, string username)
    {
        User? user = await _userRepository.GetByData(id, email, username);
        if (user is null)
            throw new KeyNotFoundException("There is no user matching supplied data.");

        return new IsUserConfirmedDTO()
        {
            IsConfirmed = user.IsConfirmed,
        };
    }

    public async Task<TokenDTO> LoginAsync(LoginUserRequest loginForm)
    {
        User? user = await _userRepository.GetByUsernameAsync(loginForm.Username);
        if(user is null) 
            throw new KeyNotFoundException("There is no user with supplied username");

        string passwordHash = _cryptographyService.Sha256Hash(loginForm.Password);
        if (!user.Password.Equals(passwordHash))
            throw new ArgumentException("Supplied password is wrong.");

        if (!user.IsConfirmed)
        {
            bool result = _emailService.SendAccountConfirmationEmail(user);
            if (result)
                throw new ArgumentException($"This account is not confirmed, confirmation link has been sent to: {user.Email}");
            else
                throw new Exception($"This account is not confirmed, confirmation link could not be sent, please contact support.");
        }

        user.LastLoginDate = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds();
        await _userRepository.UpdateAsync(user);

        TokenDTO token = GenerateToken(user);
        return token;
    }

    public async Task<RegisterUserDTO> RegisterAsync(RegisterUserRequest registerForm)
    {
        if(registerForm.Password != registerForm.Password2) 
            throw new ArgumentException("Supplied paswords are not equal.");

        User? user = await _userRepository.GetByUsernameAsync(registerForm.Username!);
        if (user is not null)
            throw new DuplicateNameException("User with supplied username already exists.");

        user = await _userRepository.GetByEmailAsync(registerForm.Email!);
        if (user is not null)
            throw new DuplicateNameException("User with supplied e-mail already exists.");

        User newUser = new()
        {
            Username = registerForm.Username!,
            Email = registerForm.Email!,
            Password = _cryptographyService.Sha256Hash(registerForm.Password!),
        };

        newUser = await _userRepository.CreateAsync(newUser);
        bool emailSent = _emailService.SendAccountConfirmationEmail(newUser);

        if(!emailSent)
            throw new Exception($"Account has been created but confirmation link could not be sent, please contact support.");

        var result = new RegisterUserDTO()
        {
            Id = newUser.Id,
            Username = newUser.Username,
            Email = newUser.Email,
        };

        return result;
    }

    public async Task<TokenDTO> RenewTokenAsync(ClaimsPrincipal claims, RenewTokenRequest renewTokenRequest)
    {
        string username = claims.FindFirstValue("Name");
        ulong userId = Convert.ToUInt64(claims.FindFirstValue("Id"));
        long expiringTime = Convert.ToInt64(claims.FindFirstValue("Exp"));

        long tokenRenewingTimeLimit = expiringTime + 7200;

        if (_timeHelper.GetCurrentUnixTimestamp() > tokenRenewingTimeLimit)
            throw new ArgumentException("Token is expired for too long.");

        if (userId != renewTokenRequest.UserId || username != renewTokenRequest.Username) 
            throw new ArgumentException("User data does not match data provided with token.");

        User? user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new KeyNotFoundException("There is no user mathcing supplied data.");

        user.LastLoginDate = (ulong)_timeHelper.GetCurrentUnixTimestamp();
        await _userRepository.UpdateAsync(user);

        TokenDTO token = GenerateToken(user);
        return token;
    }

    public async Task<SuccessDTO> ResendConfirmationMailAsync(ulong id, string email, string username)
    {
        User? user = await _userRepository.GetByData(id, email, username);
        if (user is null)
            throw new KeyNotFoundException("There is no user matching supplied data.");

        if (user.IsConfirmed) 
            throw new ArgumentException("User is already confirmed");

        bool result = _emailService.SendAccountConfirmationEmail(user);
        
        if (!result)
            throw new Exception($"Confirmation link could not be sent, please contact support.");

        return new SuccessDTO()
        {
            IsSuccess = true,
        };
    }
}
