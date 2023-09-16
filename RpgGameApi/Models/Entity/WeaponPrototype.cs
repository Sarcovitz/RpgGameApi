namespace RpgGame.Models.Entity;

/*
FINAL ENTITY
KEYS:
    1_000 - 12_999 12k
        WARRIOR:
            1_000 - 4_999 4k
        ARCHER:
            5_000 - 8_999 4k
        MAGE:
            9_000 - 12_999 4k
*/

public class WeaponPrototype : EquippablePrototype
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
    public uint MinAttackValue { get; set; } = 0;
    public uint MaxAttackValue { get; set; } = 0;
    public override bool IsStackable { get; set; } = false;
}
