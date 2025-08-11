using Templify.Application.Interfaces.Services;
using Templify.Application.Features.Auth.Commands;
using Templify.Domain.Entities;
using Templify.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Templify.Shared.Enums;

namespace Templify.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public AuthService(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IEmailService emailService,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _emailService = emailService;
    }

    public async Task<AuthResult> RegisterAsync(RegisterCommand command)
    {
        var result = new AuthResult();

        // Проверяем, существует ли пользователь с таким email или username
        var existingUserByEmail = await _userManager.FindByEmailAsync(command.Email);
        if (existingUserByEmail != null)
        {
            result.Errors.Add("Пользователь с таким email уже существует");
            return result;
        }

        var existingUserByUsername = await _userManager.FindByNameAsync(command.UserName);
        if (existingUserByUsername != null)
        {
            result.Errors.Add("Пользователь с таким именем уже существует");
            return result;
        }

        // Создаем Identity пользователя
        var identityUser = new IdentityUser
        {
            UserName = command.UserName,
            Email = command.Email,
            EmailConfirmed = false
        };

        var createResult = await _userManager.CreateAsync(identityUser, command.Password);
        if (!createResult.Succeeded)
        {
            result.Errors.AddRange(createResult.Errors.Select(e => e.Description));
            return result;
        }

        // Создаем AppUser
        var appUser = new AppUser
        {
            IdentityId = identityUser.Id,
            UserName = command.UserName,
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Role = command.Role,
            ProfileImage = "/images/default-avatar.png",
            Description = "",
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        await _context.AppUsers.AddAsync(appUser);
        await _context.SaveChangesAsync();

        // Отправляем email подтверждения
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
        var subject = "Добро пожаловать в Templify!";
        var body = $"<h1>Здравствуйте, {command.UserName}!</h1>" +
                   "<p>Вы успешно зарегистрировались на платформе Templify.</p>" +
                   "<p>Для подтверждения email перейдите по ссылке:</p>" +
                   $"<a href='https://yourdomain.com/confirm-email?userId={identityUser.Id}&token={token}'>Подтвердить email</a>";

        await _emailService.SendEmailAsync(command.Email, subject, body);

        result.Succeeded = true;
        result.UserId = identityUser.Id;
        result.UserName = command.UserName;
        result.Email = command.Email;

        return result;
    }

    public async Task<AuthResult> LoginAsync(LoginCommand command)
    {
        var result = new AuthResult();

        // Ищем пользователя по email или username
        var user = await _userManager.FindByEmailAsync(command.Login) ?? 
                   await _userManager.FindByNameAsync(command.Login);

        if (user == null)
        {
            result.Errors.Add("Неверный логин или пароль");
            return result;
        }

        // Проверяем пароль
        var signInResult = await _signInManager.PasswordSignInAsync(user, command.Password, command.RememberMe, false);
        if (!signInResult.Succeeded)
        {
            result.Errors.Add("Неверный логин или пароль");
            return result;
        }

        // Получаем AppUser
        var appUser = await _context.AppUsers.FirstOrDefaultAsync(u => u.IdentityId == user.Id);
        if (appUser == null || !appUser.IsActive)
        {
            result.Errors.Add("Аккаунт заблокирован или не найден");
            return result;
        }

        result.Succeeded = true;
        result.UserId = user.Id;
        result.UserName = appUser.UserName;
        result.Email = appUser.Email;

        return result;
    }

    public async Task<bool> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return true;
    }

    public async Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result.Succeeded;
    }

    public async Task<bool> ConfirmEmailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;

        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result.Succeeded;
    }

    public async Task<bool> ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return false;

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var subject = "Сброс пароля";
        var body = $"<h1>Сброс пароля</h1>" +
                   "<p>Для сброса пароля перейдите по ссылке:</p>" +
                   $"<a href='https://yourdomain.com/reset-password?userId={user.Id}&token={token}'>Сбросить пароль</a>";

        await _emailService.SendEmailAsync(email, subject, body);
        return true;
    }

    public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return false;

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded;
    }
}
