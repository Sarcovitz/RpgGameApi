namespace RpgGame.Models.Entity.Abstract;

/*
ENTITY BASE
*/

public abstract class ItemPrototypeBase
{
    abstract public ulong Id { get; set; }
    abstract public string Name { get; set; }
    abstract public ulong Value { get; set; }
    abstract public bool IsUsable { get; set; }
    abstract public bool IsEquippable { get; set; }
    abstract public bool IsTradable { get; set; }
    abstract public bool IsUpgradable { get; set; }
    abstract public byte UpgradeLevel { get; set; }
    abstract public uint RequiredLevel { get; set; }
    abstract public bool IsRarityDepend { get; set; }
    abstract public bool IsStackable { get; set; }
}

public enum ItemRarity
{
    Common = 0,
    Uncommon = 1,
    Rare = 2,
    Epic = 3,
    Legendary = 4,
    Mythic = 5
}
