namespace RpgGame.Models.DTO;

public class RegisterUserDTO
{
    public ulong Id { get; set; } = 0;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

}
