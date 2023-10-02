using RpgGame.Models.Entity.Abstract;

namespace RpgGame.Models.Entity;

/*
FINAL ENTITY
KEYS:
25_000 - 29_999 5k
*/

public class MaterialPrototype : ItemPrototype
{
    public override ulong Id { get; set; } = 0;
    public override string Name { get; set; } = string.Empty;
    public override ulong Value { get; set; } = 0;
    public override bool IsUsable { get; set; } = false;
    public override bool IsEquippable { get; set; } = false;
    public override bool IsTradable { get; set; } = true;
    public override bool IsUpgradable { get; set; } = false;
    public override byte UpgradeLevel { get; set; } = 0;
    public override uint RequiredLevel { get; set; } = 0;
    public override bool IsRarityDepend { get; set; } = false;
    public override bool IsStackable { get; set; } = true;
}
