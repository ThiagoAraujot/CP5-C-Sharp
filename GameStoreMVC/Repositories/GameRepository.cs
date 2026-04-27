using GameStoreMVC.Data;
using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreMVC.Repositories;

public class GameRepository : IGameRepository
{
    private readonly AppDbContext _context;

    public GameRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Game>> GetAllAsync() =>
        await _context.Games.ToListAsync();

    public async Task<IEnumerable<Game>> GetDestaqueAsync() =>
        await _context.Games.Where(g => g.Destaque).ToListAsync();

    public async Task<IEnumerable<Game>> GetByCategoriaAsync(string categoria) =>
        await _context.Games.Where(g => g.Categoria == categoria).ToListAsync();

    public async Task<Game?> GetByIdAsync(int id) =>
        await _context.Games.FindAsync(id);

    public async Task AddAsync(Game game)
    {
        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Game game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game is not null)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
    }
}
