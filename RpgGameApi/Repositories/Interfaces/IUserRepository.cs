using RpgGame.Models.Entity;

namespace RpgGame.Repositories.Interfaces;

public interface IUserRepository
{
	Task<User> CreateAsync(User user);
	Task DeleteAsync(ulong id);
    Task<User?> GetByData(ulong? id, string? email, string? username);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(ulong id);
    Task<User?> GetByUsernameAsync(string username);
    Task<int> UpdateAsync(User? user);
}
