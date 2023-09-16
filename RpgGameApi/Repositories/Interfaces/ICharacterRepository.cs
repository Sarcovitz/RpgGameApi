using RpgGame.Models.Entity;

namespace RpgGame.Repositories.Interfaces;

public interface ICharacterRepository
{
    Task<Character> AddAsync(Character character);
    Task<bool> DeleteAsync(ulong id);
    Task<Character?> GetByIdAsync(ulong id);
    Task<Character?> GetByNameAsync(string name);
    Task<List<Character>> GetByUserAsync(ulong userId);
    Task<int> UpdateAsync(Character character);
}
