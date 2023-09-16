using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RpgGame.Data;
using RpgGame.Models.Entity;
using RpgGame.Repositories.Interfaces;

namespace RpgGame.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly GameDbContext _context;

    public InventoryRepository(GameDbContext context)
    {
        _context = context;
    }

    public async Task<Inventory> AddAsync(Inventory inventory)
    {
        await _context.Inventories.AddAsync(inventory);
        await _context.SaveChangesAsync();

        return inventory;
    }

    public IQueryable<Inventory> BuildQuery(bool isExtended)
    {
        IQueryable<Inventory> query = _context.Inventories.AsQueryable();
        if (!isExtended) return query;

        query = query.Include(inventory => inventory.Slots!)
            .ThenInclude(inventorySlot => inventorySlot.Item)
            .ThenInclude(item => item.ItemPrototype);

        return query;
    }

    public async Task<Inventory> CreateAsync(Inventory inventory)
    {
        EntityEntry<Inventory> result  = _context.Inventories.Add(inventory);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<Inventory?> GetByCharacterIdAsync(ulong characterId, bool isExtended = false)
    {
        Inventory? inventory =  await BuildQuery(isExtended)
            .FirstOrDefaultAsync(inventory => inventory.CharacterId == characterId);

        if (inventory is not null && isExtended)
        {
            if(inventory.Slots is not null)
                inventory.Slots = inventory.Slots.OrderBy(slot => slot.OrderNumber).ToList();
        }

        return inventory;
    }

    public async Task<Inventory?> GetByIdAsync(ulong id, bool isExtended)
    {
        Inventory? inventory = await BuildQuery(isExtended)
            .FirstOrDefaultAsync(inventory => inventory.Id == id);

        if (inventory is not null && isExtended)
        {
            if (inventory.Slots is not null)
                inventory.Slots = inventory.Slots.OrderBy(slot => slot.OrderNumber).ToList();
        }

        return inventory;
    }
}
