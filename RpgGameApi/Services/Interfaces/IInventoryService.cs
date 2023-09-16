using RpgGame.Models.DTO;
using RpgGame.Models.Entity;
using RpgGame.Models.Request;

namespace RpgGame.Services.Interfaces;

public interface IInventoryService
{
    Task<CreateInventoryDTO> CreateAsync(ulong userId, CreateInventoryRequest createRequest);
    Task<Inventory> GetInventoryByCharacterIdAsync(ulong characterId);
    Task<Inventory> GetInventoryByIdAsync(ulong id);
}
