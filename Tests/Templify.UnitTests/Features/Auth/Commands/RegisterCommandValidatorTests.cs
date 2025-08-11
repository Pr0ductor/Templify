using FluentValidation.TestHelper;
using Templify.Application.Features.Auth.Commands.Validators;
using Templify.Application.Features.Auth.Commands;
using Templify.Shared.Enums;
using Xunit;

namespace Templify.UnitTests.Features.Auth.Commands;

public class RegisterCommandValidatorTests
{
    private readonly RegisterCommandValidator _validator;

    public RegisterCommandValidatorTests()
    {
        _validator = new RegisterCommandValidator();
    }

    [Fact]
    public void Should_Pass_When_Valid_Command()
    {
        // Arrange
        var command = new RegisterCommand
        {
            UserName = "testuser",
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "User",
            Password = "Test123!",
            ConfirmPassword = "Test123!",
            Role = UserRole.User
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Fail_When_UserName_Empty()
    {
        // Arrange
        var command = new RegisterCommand
        {
            UserName = "",
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "User",
            Password = "Test123!",
            ConfirmPassword = "Test123!",
            Role = UserRole.User
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserName);
    }

    [Fact]
    public void Should_Fail_When_Email_Invalid()
    {
        // Arrange
        var command = new RegisterCommand
        {
            UserName = "testuser",
            Email = "invalid-email",
            FirstName = "Test",
            LastName = "User",
            Password = "Test123!",
            ConfirmPassword = "Test123!",
            Role = UserRole.User
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_Fail_When_Passwords_Do_Not_Match()
    {
        // Arrange
        var command = new RegisterCommand
        {
            UserName = "testuser",
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "User",
            Password = "Test123!",
            ConfirmPassword = "Different123!",
            Role = UserRole.User
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
    }
}
