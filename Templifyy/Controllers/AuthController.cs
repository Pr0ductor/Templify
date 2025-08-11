using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Templify.Application.Features.Auth.Commands;
using Templify.Application.Interfaces.Services;
using Templify.Shared.Enums;

namespace Templifyy.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(new LoginCommand());
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        var result = await _authService.LoginAsync(command);
        
        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = $"Добро пожаловать, {result.UserName}!";
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error);
        }

        return View(command);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(new RegisterCommand());
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        var result = await _authService.RegisterAsync(command);
        
        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = $"Аккаунт успешно создан! Проверьте email для подтверждения.";
            return RedirectToAction("Login");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error);
        }

        return View(command);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        TempData["SuccessMessage"] = "Вы успешно вышли из системы";
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
