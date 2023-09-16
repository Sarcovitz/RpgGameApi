using System.ComponentModel.DataAnnotations;

namespace RpgGame.Models.Request
{
    public class RenewTokenRequest
    {
        [Required]
        [Range(1, ulong.MaxValue)]
        public ulong? UserId { get; set; } = 0;
        [Required]
        [MinLength(1)]
        public string? Username { get; set; } = string.Empty;
    }
}
