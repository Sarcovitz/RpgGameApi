using RpgGame.Models.DTO;
using RpgGame.Models.Entity;
using RpgGame.Repositories.Interfaces;
using RpgGame.Services.Interfaces;
using System.Security.Claims;

namespace RpgGame.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDTO> GetCurrentUserAsync(ClaimsPrincipal claims)
    {
        ulong userId = Convert.ToUInt64(claims.FindFirstValue("Id"));

        User? user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new KeyNotFoundException("Provided token does not match to any user.");

        return new UserDTO()
        {
            Username = user.Username,
            Email = user.Email,
            CharacterSlots = user.CharacterSlots,
            CreationDate = user.CreationDate,
            IsConfirmed = user.IsConfirmed,
            LastLoginDate = user.LastLoginDate,
            Role = user.Role,            
        };
    }
}
