using System.Text.Json.Serialization;

namespace RpgGame.Models.Entity.Bases;

public abstract class ItemBase
{
    abstract public Guid Id { get; set; }

    abstract public ulong ItemPrototypeId { get; set; }
    abstract public ItemPrototypeBase? ItemPrototype { get; set; }

    abstract public ushort Quantity { get; set; }

    abstract public Guid? InventorySlotId { get; set; }
    [JsonIgnore]
    abstract public InventorySlot? InventorySlot { get; set; }
}
