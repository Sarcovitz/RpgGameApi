using RpgGame.Models.Entity;

namespace RpgGame.Models.DTO;

public class UserDTO
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ulong CreationDate { get; set; } = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds();
    public byte CharacterSlots { get; set; } = 3;
    public bool IsConfirmed { get; set; } = false;
    public ulong LastLoginDate { get; set; } = 0;
    public UserRole Role { get; set; } = UserRole.Player;
}
