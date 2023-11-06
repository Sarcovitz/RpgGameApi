using System.Text.Json.Serialization;
using RpgGame.Models.Entity.Bases;

namespace RpgGame.Models.Entity;

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
