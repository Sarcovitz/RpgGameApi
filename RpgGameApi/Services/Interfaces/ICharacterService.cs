using RpgGame.Models.DTO;
using RpgGame.Models.Entity;
using RpgGame.Models.Request;

namespace RpgGame.Services.Interfaces;

public interface ICharacterService
{
    Task<CreateCharacterDTO> CreateAsync(ulong userId, CreateCharacterRequest request);
    Task<SuccessDTO> DeleteAsync(ulong characterId);
    Task<List<Character>> GetAllAsync(ulong userId);
    Task<Character> GetByIdAsync(ulong characterId);
}
