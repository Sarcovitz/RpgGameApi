using System.ComponentModel.DataAnnotations;

namespace RpgGame.Models.Request;

public class RegisterUserRequest
{
    [MaxLength(16)]
    [MinLength(3)]
    [RegularExpression(@"^[a-zA-Z0-9]+$")]
    [Required]
    public string? Username { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string? Email { get; set; } = string.Empty;
    [MaxLength(16)]
    [MinLength(6)]
    [Required]
    public string? Password { get; set; } = string.Empty;
    [Required]
    [MinLength(1)]
    public string? Password2 { get; set; } = string.Empty;
}
