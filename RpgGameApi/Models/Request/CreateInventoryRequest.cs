using System.ComponentModel.DataAnnotations;

namespace RpgGame.Models.Request;

public class CreateInventoryRequest
{
    [Required]
    [Range(1, ulong.MaxValue)]
    public ulong CharacterId { get; set; } = 0;
}
