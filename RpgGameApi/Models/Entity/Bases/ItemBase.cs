using System.Text.Json.Serialization;

namespace RpgGame.Models.Entity.Bases;

public abstract class ItemBase
{
    public Guid Id { get; set; }

    public ulong ItemPrototypeId { get; set; }
    public ItemPrototypeBase? ItemPrototype { get; set; }

    public ushort Quantity { get; set; } = 1;

    public Guid? InventorySlotId { get; set; }
    [JsonIgnore]
    public InventorySlot? InventorySlot { get; set; }
}
