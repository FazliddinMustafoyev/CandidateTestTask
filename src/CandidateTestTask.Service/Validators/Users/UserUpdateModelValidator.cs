using FluentValidation;
using CandidateTestTask.Service.Helpers;
using CandidateTestTask.Service.DTOs.Users;

namespace CandidateTestTask.Service.Validators.Users;

public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
{
    public UserUpdateModelValidator()
    {
        RuleFor(user => user.FirstName)
            .NotNull()
            .WithMessage(user => $"{nameof(user.FirstName)} is not specified");

        RuleFor(user => user.LastName)
            .NotNull()
            .WithMessage(user => $"{nameof(user.LastName)} is not specified");

        RuleFor(user => user.Description)
            .NotNull()
            .WithMessage(user => $"{nameof(user.Description)} is not specified");
    }
}
