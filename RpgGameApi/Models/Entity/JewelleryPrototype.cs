namespace RpgGame.Models.Entity.Bases;

public class JewelleryPrototype : ItemPrototypeBase
{
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
    public override bool IsStackable { get; set; } = false;
}
