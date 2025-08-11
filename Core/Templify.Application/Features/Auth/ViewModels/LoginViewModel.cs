using System.ComponentModel.DataAnnotations;

namespace Templify.Application.Features.Auth.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Логин или email обязателен")]
    [Display(Name = "Логин или Email")]
    public string UserNameOrEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Пароль обязателен")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Запомнить меня")]
    public bool RememberMe { get; set; }
}
