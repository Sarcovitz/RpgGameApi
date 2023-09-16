using RpgGame.Data.Static;
using System.Text.Json.Serialization;

namespace RpgGame.Models.Entity;

public class Inventory
{
    public ulong Id { get; set; }
    public byte InventoryPages { get; set; } = GeneralData.BASE_INVENTORY_PAGES;
    public ulong Money { get; set; } = 0;

    public List<InventorySlot>? Slots { get; set; }

    public ulong CharacterId { get; set; }
    [JsonIgnore]
    public Character? Character { get; set; }

    public static Inventory GetBaseInventory(ulong? characterId)
    {
        Inventory baseInventory = new Inventory();
        baseInventory.Slots = new();
        for (ushort i = 0; i < GeneralData.BASE_INVENTORY_PAGES * GeneralData.INVENTORY_PAGE_SLOTS; i++) 
        {
            baseInventory.Slots.Add(new InventorySlot()
            {
                OrderNumber = i,
            });
        }

        if(characterId.HasValue) 
            baseInventory.CharacterId = characterId.Value;

        return baseInventory;
    }
}
