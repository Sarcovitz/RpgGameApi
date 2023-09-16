using RpgGame.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace RpgGame.Models.Request;

public class CreateCharacterRequest
{
    [MaxLength(16)]
    [MinLength(3)]
    [RegularExpression(@"^[a-zA-Z0-9]+$")]
    [Required]
    public string? Name { get; set; } = string.Empty;
    [Required]
    [EnumDataType(typeof(CharacterClass))]
    [Range((int)CharacterClass.Warrior, (int) CharacterClass.Mage)]
    public CharacterClass Class { get; set; } = CharacterClass.Warrior;
}
