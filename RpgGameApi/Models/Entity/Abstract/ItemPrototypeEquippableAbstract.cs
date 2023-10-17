namespace RpgGame.Models.Entity.Abstract;

//PE
public abstract class ItemPrototypeEquippableAbstract : ItemPrototypeBase
{
    abstract public EquippableBy EquippableBy { get; set; }
}

public enum EquippableBy
{
    All = -1,
    Warrior = 0,
    Archer = 1,
    Mage = 2,
}