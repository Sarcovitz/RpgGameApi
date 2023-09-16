using RpgGame.Models.DTO;
using RpgGame.Models.Entity;
using RpgGame.Models.Request;

namespace RpgGame.Services.Interfaces;

public interface ICharacterService
{
    Task<CreateCharacterDTO> CreateAsync(ulong userId, CreateCharacterRequest request);
    Task<SuccessDTO> DeleteAsync(ulong characterId);
    Task<List<Character>> GetAllAsync(ulong userId);
    Character GetBaseCharacter(CreateCharacterRequest request, ulong userId);
    ulong GetBaseDexterity(CharacterClass characterClass);
    ulong GetBaseIntelligence(CharacterClass characterClass);
    ulong GetBaseStrenght(CharacterClass characterClass);
    ulong GetBaseVitality(CharacterClass characterClass);
    Task<Character> GetByIdAsync(ulong characterId);
}
