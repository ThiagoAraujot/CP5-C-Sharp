using GameStoreMVC.Models;

namespace GameStoreMVC.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
    Task AddAsync(Usuario usuario);
}
