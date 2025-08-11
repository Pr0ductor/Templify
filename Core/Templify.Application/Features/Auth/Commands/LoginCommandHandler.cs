using MediatR;
using Microsoft.AspNetCore.Identity;
using Templify.Application.Features.Auth.Commands;

namespace Templify.Application.Features.Auth.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public LoginCommandHandler(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = new LoginResult();

        // Ищем пользователя по email или username
        var user = await _userManager.FindByEmailAsync(request.UserNameOrEmail) ??
                  await _userManager.FindByNameAsync(request.UserNameOrEmail);

        if (user == null)
        {
            result.Errors.Add("Неверный логин или пароль");
            return result;
        }

        // Проверяем пароль
        var signInResult = await _signInManager.PasswordSignInAsync(
            user, 
            request.Password, 
            request.RememberMe, 
            lockoutOnFailure: false);

        if (!signInResult.Succeeded)
        {
            result.Errors.Add("Неверный логин или пароль");
            return result;
        }

        result.Succeeded = true;
        result.UserId = user.Id;
        result.UserName = user.UserName;
        result.Email = user.Email;

        return result;
    }
}
