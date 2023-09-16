using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RpgGame.Data;
using RpgGame.Models.Entity;
using RpgGame.Repositories.Interfaces;

namespace RpgGame.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private readonly GameDbContext _context;
    public CharacterRepository(GameDbContext context)
    {
        _context = context;
    }

    public async Task<Character> AddAsync(Character character)
    {
        await _context.Characters.AddAsync(character);
        await _context.SaveChangesAsync();

        return character;
    }

    public async Task<bool> DeleteAsync(ulong id)
    {
        Character? character = await GetByIdAsync(id) ?? 
            throw new KeyNotFoundException("Character with ID:{id} not found or is already deleted.");

        _context.Characters.Remove(character);        
        return await _context.SaveChangesAsync() > 0;
    }

    public async  Task<Character?> GetByIdAsync(ulong id) 
        => await _context.Characters.FirstOrDefaultAsync(character => character.Id == id);

    public async Task<Character?> GetByNameAsync(string name)
        => await _context.Characters.FirstOrDefaultAsync(character => character.Name.ToLower() == name.ToLower());

    public async Task<List<Character>> GetByUserAsync(ulong userId) 
        => await _context.Characters.Where(character => character.UserId == userId).ToListAsync();

    public Task<int> UpdateAsync(Character user)
    {
        throw new NotImplementedException();
    }
}
