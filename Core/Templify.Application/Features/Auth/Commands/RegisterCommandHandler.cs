using MediatR;
using Microsoft.AspNetCore.Identity;
using Templify.Domain.Entities;
using Templify.Application.Features.Auth.Commands;

namespace Templify.Application.Features.Auth.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResult>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IAppUserRepository _appUserRepository;

    public RegisterCommandHandler(
        UserManager<IdentityUser> userManager,
        IAppUserRepository appUserRepository)
    {
        _userManager = userManager;
        _appUserRepository = appUserRepository;
    }

    public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var result = new RegisterResult();

        // Проверяем, что пароли совпадают
        if (request.Password != request.ConfirmPassword)
        {
            result.Errors.Add("Пароли не совпадают");
            return result;
        }

        // Проверяем, что пользователь с таким email не существует
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            result.Errors.Add("Пользователь с таким email уже существует");
            return result;
        }

        // Проверяем, что пользователь с таким username не существует
        var existingUserByUsername = await _userManager.FindByNameAsync(request.UserName);
        if (existingUserByUsername != null)
        {
            result.Errors.Add("Пользователь с таким именем уже существует");
            return result;
        }

        // Создаем Identity пользователя
        var identityUser = new IdentityUser
        {
            UserName = request.UserName,
            Email = request.Email,
            EmailConfirmed = true // Для простоты подтверждаем email автоматически
        };

        var createResult = await _userManager.CreateAsync(identityUser, request.Password);
        if (!createResult.Succeeded)
        {
            result.Errors.AddRange(createResult.Errors.Select(e => e.Description));
            return result;
        }

        // Создаем AppUser
        var appUser = new AppUser
        {
            IdentityId = identityUser.Id,
            UserName = request.UserName,
            Email = request.Email,
            FullName = request.FullName,
            Role = request.Role,
            CreatedAt = DateTime.UtcNow
        };

        await _appUserRepository.AddAsync(appUser);
        await _appUserRepository.SaveChangesAsync();

        result.Succeeded = true;
        result.UserId = identityUser.Id;

        return result;
    }
}
