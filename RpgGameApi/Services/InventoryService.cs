using RpgGame.Models.DTO;
using RpgGame.Models.Entity;
using RpgGame.Models.Request;
using RpgGame.Repositories.Interfaces;
using RpgGame.Services.Interfaces;
using System.Data;

namespace RpgGame.Services;

public class InventoryService : IInventoryService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryService(IInventoryRepository inventoryRepository, 
        ICharacterRepository characterRepository)
    {
        _inventoryRepository = inventoryRepository;
        _characterRepository = characterRepository;
    }

    public async Task<CreateInventoryDTO> CreateAsync(ulong userId, CreateInventoryRequest createRequest)
    {
        Character? character = await _characterRepository.GetByIdAsync(createRequest.CharacterId);
        if (character == null)
            throw new KeyNotFoundException($"There is no character with supplied ID: {createRequest.CharacterId}");

        if (character.UserId != userId)
            throw new ArgumentException($"Supplied character ID: {createRequest.CharacterId} does not fit user making request.");

        Inventory? inventory = await _inventoryRepository.GetByCharacterIdAsync(createRequest.CharacterId, false);
        if (inventory is not null)
            throw new DuplicateNameException($"Supplied character (ID: {createRequest.CharacterId}) already has a created inventory.");

        Inventory newInventory = Inventory.GetBaseInventory(createRequest.CharacterId);
        newInventory = await _inventoryRepository.AddAsync(newInventory);

        return new CreateInventoryDTO()
        {
            Id = newInventory.Id,
            CharacterId = newInventory.CharacterId,
        };
    }

    public async Task<Inventory> GetInventoryByIdAsync(ulong id)
    {
        Inventory? inventory = await _inventoryRepository.GetByIdAsync(id, true);
        if (inventory is null)
            throw new KeyNotFoundException($"There is no inventory with Id: {id}");

        return inventory;
    }
    
    public async Task<Inventory> GetInventoryByCharacterIdAsync(ulong characterId)
    {
        Inventory? inventory = await _inventoryRepository.GetByCharacterIdAsync(characterId, true);
        if(inventory is null)
            throw new KeyNotFoundException($"There is no inventory with character ID: {characterId}");

        return inventory;
    }
}
