using RpgGame.Data.Static;
using System.Text.Json.Serialization;

namespace RpgGame.Models.Entity;

public class User
{
    public ulong Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [JsonIgnore]
    public string Password { get; set; } = string.Empty;
    public ulong CreationDate { get; set; } = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds();
    public byte CharacterSlots { get; set; } = GeneralData.BASE_CHARACTER_SLOTS;
    public bool IsConfirmed { get; set; } = false;
    public ulong LastLoginDate { get; set; } = 0;
    public UserRole Role { get; set; } = UserRole.Player;

    public List<Character>? Characters { get; set; }
}

public enum UserRole
{
    Player = 0,
    Tester = 1,
    Moderator = 2,
    Developer = 3,
    GameMaster = 4,
    Admin = 5,
}
