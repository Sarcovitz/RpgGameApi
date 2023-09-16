using RpgGame.Models.DTO;
using System.Security.Claims;

namespace RpgGame.Services.Interfaces;

public interface IUserService
{
    public Task<UserDTO> GetCurrentUserAsync(ClaimsPrincipal claims);
}
