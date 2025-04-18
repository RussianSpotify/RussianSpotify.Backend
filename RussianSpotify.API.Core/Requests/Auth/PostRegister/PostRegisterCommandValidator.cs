#region

using FluentValidation;
using RussianSpotify.API.Core.Enums;

#endregion

namespace RussianSpotify.API.Core.Requests.Auth.PostRegister;

/// <summary>
///     Валидатор для <see cref="PostRegisterCommand" />
/// </summary>
public class PostRegisterCommandValidator : AbstractValidator<PostRegisterCommand>
{
    public PostRegisterCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Email"));

        RuleFor(command => command.Email).EmailAddress().WithMessage(AuthErrorMessages.InvalidEmailFormat);

        RuleFor(command => command.UserName)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("User Name"));

        RuleFor(command => command.Password)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Password"));

        RuleFor(command => command.Password).MinimumLength(8)
            .WithMessage(AuthErrorMessages.ShortPassword(8));

        RuleFor(command => command.PasswordConfirm)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Password Confirm"));

        RuleFor(command => command.Password)
            .Equal(command => command.PasswordConfirm).WithMessage(AuthErrorMessages.PasswordIsNotConfirmed);
    }
}