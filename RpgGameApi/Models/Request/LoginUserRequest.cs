using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RpgGame.Models.Request;

public class LoginUserRequest
{
    [MinLength(1)]
    [NotNull]
    [Required]
	public string? Username { get; set; } = string.Empty;
    [MinLength(1)]
    [NotNull]
    [Required]
    public string? Password { get; set; } = string.Empty;
}
