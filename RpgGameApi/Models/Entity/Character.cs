using RpgGame.Data.Static;
using RpgGame.Models.Request;
using System.Text.Json.Serialization;

namespace RpgGame.Models.Entity;

public class Character
{
    public ulong Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ulong CreationDate { get; set; } = (ulong) DateTimeOffset.Now.ToUnixTimeSeconds();
    public CharacterClass Class { get; set; } = CharacterClass.Warrior;
    public uint Level { get; set; } = 1;
    public ulong Experience { get; set; } = 0;
    public ulong RequiredExperience { get; set; } = 300;
    public ulong Strength { get; set; } = 0;
    public ulong Dexterity { get; set; } = 0;
    public ulong Intelligence { get; set; } = 0;
    public ulong Vitality { get; set; } = 0;

    public ulong? UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }

    public Inventory? Inventory { get; set; }

    public static Character GetBaseCharacter(CreateCharacterRequest request, ulong userId)
    {
        Character character = new();
        character.UserId = userId;
        character.Name = request.Name!;
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

    public static ulong GetBaseDexterity(CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Archer => 10,
            CharacterClass.Warrior => 4,
            CharacterClass.Mage => 7,
            _ => throw new ArgumentException("Unknown charcter class.")
        };
    }

    public static ulong GetBaseIntelligence(CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Archer => 3,
            CharacterClass.Warrior => 3,
            CharacterClass.Mage => 10,
            _ => throw new ArgumentException("Unknown charcter class.")
        };
    }

    public static ulong GetBaseStrenght(CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Archer => 6,
            CharacterClass.Warrior => 10,
            CharacterClass.Mage => 3,
            _ => throw new ArgumentException("Unknown charcter class.")
        };
    }

    public static ulong GetBaseVitality(CharacterClass characterClass)
    {
        return characterClass switch
        {
            CharacterClass.Archer => 5,
            CharacterClass.Warrior => 7,
            CharacterClass.Mage => 4,
            _ => throw new ArgumentException("Unknown charcter class.")
        };
    }
}

public enum CharacterClass
{
    Warrior = 10,
    Archer = 20,
    Mage = 30,
}
