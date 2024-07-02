using FluentValidation;
using CandidateTestTask.Service.Helpers;
using CandidateTestTask.Service.DTOs.Users;

namespace CandidateTestTask.Service.Validators.Users;

public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
{
    public UserCreateModelValidator()
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

        RuleFor(user => user.Email)
            .Must(ValidationHelper.IsEmailValid);

        RuleFor(user => user.Password)
            .Must(ValidationHelper.IsPasswordHard);
    }
}