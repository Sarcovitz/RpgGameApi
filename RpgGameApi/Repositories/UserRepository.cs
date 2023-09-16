using Microsoft.EntityFrameworkCore;
using RpgGame.Data;
using RpgGame.Models.Entity;
using RpgGame.Repositories.Interfaces;

namespace RpgGame.Repositories;

public class UserRepository : IUserRepository
{
    private readonly GameDbContext _context;
    public UserRepository(GameDbContext gameDbContext)
    {
        _context = gameDbContext;
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task DeleteAsync(ulong id)
    {
        User? user = await GetByIdAsync(id) ?? 
            throw new KeyNotFoundException("User with ID:{id} not found or is already deleted.");

        _context.Users.Remove(user);
    }

    public Task<User?> GetByData(ulong? id, string? email, string? username)
    {
        var query = _context.Users.AsQueryable();
        if (id.HasValue) query = query.Where(user => user.Id == id);
        if (!string.IsNullOrWhiteSpace(email)) 
            query = query.Where(user => user.Email.ToUpper() == email.ToUpper());
        if (!string.IsNullOrWhiteSpace(username)) 
            query = query.Where(user => user.Username.ToUpper() == username.ToUpper());

        return query.FirstOrDefaultAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(user => user.Email.ToUpper() == email.ToUpper());

    public async Task<User?> GetByIdAsync(ulong id) 
        => await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

    public async Task<User?> GetByUsernameAsync(string username) 
        => await _context.Users.FirstOrDefaultAsync(user => user.Username.ToUpper() == username.ToUpper());

    public Task<int> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return _context.SaveChangesAsync();
    }
}
