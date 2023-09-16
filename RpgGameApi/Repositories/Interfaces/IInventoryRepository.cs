using RpgGame.Models.Entity;

namespace RpgGame.Repositories.Interfaces;

public interface IInventoryRepository
{
    Task<Inventory> AddAsync(Inventory inventory);
    IQueryable<Inventory> BuildQuery(bool isExtended);
    Task<Inventory> CreateAsync(Inventory inventory);
    Task<Inventory?> GetByCharacterIdAsync(ulong characterId, bool isExtended);
    Task<Inventory?> GetByIdAsync(ulong id, bool isExtended);
}
