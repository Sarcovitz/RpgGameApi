using Microsoft.IdentityModel.Tokens;
using RpgGame.Models.DTO;
using RpgGame.Models.Entity;
using RpgGame.Models.Request;
using System.Security.Claims;

namespace RpgGame.Services.Interfaces;

public interface IAuthService
{
    Task<SuccessDTO> ConfirmUserAsync(ulong id, string email, string username);
    TokenDTO GenerateToken(User user);
    Task<IsUserConfirmedDTO> IsUserConfirmedAsync(ulong id, string email, string username);
    Task<TokenDTO> LoginAsync(LoginUserRequest loginForm);
    Task<RegisterUserDTO> RegisterAsync(RegisterUserRequest registerForm);
    Task<TokenDTO> RenewTokenAsync(ClaimsPrincipal user, RenewTokenRequest renewTokenRequest);
    Task<SuccessDTO> ResendConfirmationMailAsync(ulong id, string email, string username);
}
