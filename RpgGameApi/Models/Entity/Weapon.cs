using RpgGame.Models.Entity.Bases;

namespace RpgGame.Models.Entity;

public class Weapon : ItemBase
{
    public override Guid Id { get; set; }
    public override ulong ItemPrototypeId { get; set; }
    public override ItemPrototypeBase? ItemPrototype { get; set; }
    public override ushort Quantity { get; set; }
    public override Guid? InventorySlotId { get; set; }
    public override InventorySlot? InventorySlot { get; set; }
}
