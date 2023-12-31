﻿using System.Text.Json.Serialization;
using RpgGame.Models.Entity.Bases;

namespace RpgGame.Models.Entity;

public class InventorySlot
{
    public Guid Id { get; set; }
    public ushort OrderNumber { get; set; }

    public Guid? ItemId { get; set; }
    public ItemBase? Item { get; set; }

    public ulong InventoryId { get; set; }
    [JsonIgnore]
    public Inventory? Inventory { get; set; }
}
