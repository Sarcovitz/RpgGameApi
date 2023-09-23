using RpgGame.Data.Static;
using RpgGame.Models.DTO;
using RpgGame.Models.Entity;
using RpgGame.Models.Request;
using RpgGame.Repositories.Interfaces;
using RpgGame.Services.Interfaces;

namespace RpgGame.Services;

public class CharacterService : ICharacterService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IUserRepository _userRepository;
    public CharacterService(ICharacterRepository characterRepository,
        IUserRepository userRepository)
    {
        _characterRepository = characterRepository;
        _userRepository = userRepository;
    }

    public async Task<CreateCharacterDTO> CreateAsync(ulong userId, CreateCharacterRequest request)
    {
        if (!Enum.IsDefined(request.Class)) 
            throw new ArgumentException("Supplied character class is wrong.");

        Character? existingCharacter = await _characterRepository.GetByNameAsync(request.Name!);
        if (existingCharacter is not null)
            throw new ArgumentException("Character with supplied name already exists.");

        User? user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new Exception("User cannot be obtained from supplied token");

        List<Character> userCharacters = await _characterRepository.GetByUserAsync(userId);
        if (user.CharacterSlots <= userCharacters.Count)
            throw new ArgumentException($"User character limit ({user.CharacterSlots}) has been reached.");

        Character character = Character.GetBaseCharacter(request, userId);
        character = await _characterRepository.AddAsync(character);

        return new CreateCharacterDTO()
        {
            Id = character.Id,
            Name = character.Name,
        };
    }

    public async Task<SuccessDTO> DeleteAsync(ulong characterId)
    {
        bool result = await _characterRepository.DeleteAsync(characterId);
        if (!result)
            throw new Exception("Error occured while deleting character, try again or contact with support.");

        return new SuccessDTO() 
        { 
            IsSuccess = result 
        };
    }

    public async Task<List<Character>> GetAllAsync(ulong userId)
    {
        List<Character> result = await _characterRepository.GetByUserAsync(userId);

        return result;
    }

    public async Task<Character> GetByIdAsync(ulong characterId)
    {
        Character? character = await _characterRepository.GetByIdAsync(characterId) 
            ?? throw new KeyNotFoundException("There is no character with this Id");

        return character;
    }
}
