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

        User? user = await _userRepository.GetByIdAsync(userId) ??
            throw new Exception("User cannot be obtained from supplied token");

        List<Character> userCharacters = await _characterRepository.GetByUserAsync(userId);
        if (user.CharacterSlots <= userCharacters.Count)
            throw new ArgumentException($"User character limit ({user.CharacterSlots}) has been reached.");

        Character character = GetBaseCharacter(request, userId);
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

        return new SuccessDTO() { IsSuccess = result };
    }

    public async Task<List<Character>> GetAllAsync(ulong userId)
    {
        List<Character> result = await _characterRepository.GetByUserIdAsync(userId);

        return result;
    }

    public Character GetBaseCharacter(CreateCharacterRequest request, ulong userId)
    {
        Character character = new();
        character.UserId = userId;
        character.Name = request.Name;
        character.Class = request.Class;

        character.Strength = GetBaseStrenght(request.Class);
        character.Vitality = GetBaseVitality(request.Class);
        character.Intelligence = GetBaseIntelligence(request.Class);
        character.Dexterity = GetBaseDexterity(request.Class);

        character.RequiredExperience = LevelThresholds.Values.First(val => val.Level == 1).RequiredExperience;
        
        Inventory inventory = Inventory.GetBaseInventory(null);
        character.Inventory = inventory;

        return character;
    }

    public ulong GetBaseDexterity(CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Archer => 10,
            CharacterClass.Warrior => 4,
            CharacterClass.Mage => 7,
            _ => throw new ArgumentException("Unknown charcter class.")
        };
    }

    public ulong GetBaseIntelligence(CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Archer => 3,
            CharacterClass.Warrior => 3,
            CharacterClass.Mage => 10,
            _ => throw new ArgumentException("Unknown charcter class.")
        };
    }

    public ulong GetBaseStrenght(CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Archer => 6,
            CharacterClass.Warrior => 10,
            CharacterClass.Mage => 3,
            _ => throw new ArgumentException("Unknown charcter class.")
        };
    }

    public ulong GetBaseVitality(CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Archer => 5,
            CharacterClass.Warrior => 7,
            CharacterClass.Mage => 4,
            _ => throw new ArgumentException("Unknown charcter class.")
        };
    }

    public async Task<Character> GetByIdAsync(ulong characterId)
    {
        Character? character = await _characterRepository.GetByIdAsync(characterId) 
            ?? throw new KeyNotFoundException("There is no character with this Id");

        return character;
    }
}
