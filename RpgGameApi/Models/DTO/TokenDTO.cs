namespace RpgGame.Models.DTO;

public class TokenDTO
{
    public ulong UserId { get; set; } = 0;
    public string Username { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public long AccessTokenExpirationDate { get; set; }
    public bool IsConfirmed { get; set; } = false;
    public byte Role { get; set; } = 0;
}
