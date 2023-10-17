using RpgGame.Models.Entity.Abstract;

namespace RpgGame.Models.Entity;

/*
FINAL ENTITY
KEYS:
    13_000 - 24_999 12k
        WARRIOR:
            13_000 - 16_999 4k
        ARCHER:
            17_000 - 20_999 4k
        MAGE:
            21_000 - 24_999 4k
*/

public class ArmorPrototype : ItemPrototypeEquippableAbstract
{
    public override EquippableBy EquippableBy { get; set; } = EquippableBy.All;
    public override ulong Id { get; set; } = 0;
    public override string Name { set; get; } = string.Empty;
    public override ulong Value { get; set; } = 1;
    public override bool IsUsable { get; set; } = false;
    public override bool IsEquippable { get; set; } = true;
    public override bool IsTradable { get; set; } = true;
    public override bool IsUpgradable { get; set; } = true;
    public override byte UpgradeLevel { get; set; } = 0;
    public override uint RequiredLevel { get; set; } = 0;
    public override bool IsRarityDepend { get; set; } = true;
    public uint DefenseValue { get; set; } = 0;
    public override bool IsStackable { get; set; } = false;
}
