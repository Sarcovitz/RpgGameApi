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
}

public enum CharacterClass
{
    Warrior = 10,
    Archer = 20,
    Mage = 30,
}
