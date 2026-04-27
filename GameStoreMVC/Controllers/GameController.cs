using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using GameStoreMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreMVC.Controllers;

[Authorize(Roles = "Admin")]
public class GameController : Controller
{
    private readonly IGameRepository _gameRepository;

    public GameController(IGameRepository gameRepository) =>
        _gameRepository = gameRepository;

    public async Task<IActionResult> Index()
    {
        var games = await _gameRepository.GetAllAsync();
        return View(games);
    }

    [HttpGet]
    public IActionResult Criar() => View(new GameViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(GameViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var game = new Game
        {
            Nome = model.Nome,
            DescricaoCurta = model.DescricaoCurta,
            Preco = model.Preco,
            UrlCapa = model.UrlCapa,
            Categoria = model.Categoria,
            Destaque = model.Destaque
        };

        await _gameRepository.AddAsync(game);
        TempData["Success"] = $"'{game.Nome}' cadastrado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Editar(int id)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        if (game is null) return NotFound();

        var model = new GameViewModel
        {
            Id = game.Id,
            Nome = game.Nome,
            DescricaoCurta = game.DescricaoCurta,
            Preco = game.Preco,
            UrlCapa = game.UrlCapa,
            Categoria = game.Categoria,
            Destaque = game.Destaque
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(GameViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var game = new Game
        {
            Id = model.Id,
            Nome = model.Nome,
            DescricaoCurta = model.DescricaoCurta,
            Preco = model.Preco,
            UrlCapa = model.UrlCapa,
            Categoria = model.Categoria,
            Destaque = model.Destaque
        };

        await _gameRepository.UpdateAsync(game);
        TempData["Success"] = $"'{game.Nome}' atualizado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Excluir(int id)
    {
        await _gameRepository.DeleteAsync(id);
        TempData["Success"] = "Jogo excluído com sucesso!";
        return RedirectToAction(nameof(Index));
    }
}
