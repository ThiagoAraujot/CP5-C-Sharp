using GameStoreMVC.Models;

namespace GameStoreMVC.Interfaces;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetAllAsync();
    Task<IEnumerable<Game>> GetDestaqueAsync();
    Task<IEnumerable<Game>> GetByCategoriaAsync(string categoria);
    Task<Game?> GetByIdAsync(int id);
    Task AddAsync(Game game);
    Task UpdateAsync(Game game);
    Task DeleteAsync(int id);
}
