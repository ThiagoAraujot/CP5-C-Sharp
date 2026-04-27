using GameStoreMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreMVC.Controllers;

public class HomeController : Controller
{
    private readonly IGameRepository _gameRepository;

    public HomeController(IGameRepository gameRepository) =>
        _gameRepository = gameRepository;

    public async Task<IActionResult> Index(string? categoria)
    {
        var games = string.IsNullOrEmpty(categoria)
            ? await _gameRepository.GetDestaqueAsync()
            : await _gameRepository.GetByCategoriaAsync(categoria);

        ViewBag.Categoria = categoria;
        return View(games);
    }
}
