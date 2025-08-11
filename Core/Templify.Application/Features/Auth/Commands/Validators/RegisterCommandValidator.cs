using FluentValidation;
using Templify.Application.Features.Auth.Commands;

namespace Templify.Application.Features.Auth.Commands.Validators;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Имя пользователя обязательно")
            .Length(2, 50).WithMessage("Имя пользователя должно содержать от 2 до 50 символов")
            .Matches(@"^[a-zA-Z0-9_-]+$").WithMessage("Имя пользователя может содержать только буквы, цифры, дефисы и подчеркивания");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email обязателен")
            .EmailAddress().WithMessage("Неверный формат email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обязателен")
            .Length(6, 100).WithMessage("Пароль должен содержать от 6 до 100 символов")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("Пароль должен содержать хотя бы одну строчную букву, одну заглавную букву и одну цифру");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Пароли не совпадают");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Неверная роль");

        RuleFor(x => x.FirstName)
            .MaximumLength(50).WithMessage("Имя не может превышать 50 символов");

        RuleFor(x => x.LastName)
            .MaximumLength(50).WithMessage("Фамилия не может превышать 50 символов");
    }
}
