using System.Security.Claims;
using GameStoreMVC.Interfaces;
using GameStoreMVC.Models;
using GameStoreMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreMVC.Controllers;

public class AccountController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;

    public AccountController(IUsuarioRepository usuarioRepository) =>
        _usuarioRepository = usuarioRepository;

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var usuario = await _usuarioRepository.GetByEmailAsync(model.Email);
        if (usuario is null || !BCrypt.Net.BCrypt.Verify(model.Senha, usuario.SenhaHash))
        {
            ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new(ClaimTypes.Name, usuario.Nome),
            new(ClaimTypes.Email, usuario.Email),
            new(ClaimTypes.Role, usuario.Role)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            });

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult CriarConta() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CriarConta(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        if (await _usuarioRepository.EmailExistsAsync(model.Email))
        {
            ModelState.AddModelError("Email", "Este e-mail já está cadastrado.");
            return View(model);
        }

        var usuario = new Usuario
        {
            Nome = model.Nome,
            Email = model.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(model.Senha),
            Role = "User"
        };

        await _usuarioRepository.AddAsync(usuario);
        TempData["Success"] = "Conta criada com sucesso! Faça o login.";
        return RedirectToAction(nameof(Login));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
