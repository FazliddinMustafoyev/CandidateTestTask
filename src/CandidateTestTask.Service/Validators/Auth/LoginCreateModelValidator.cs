using CandidateTestTask.Service.DTOs.Auth;
using CandidateTestTask.Service.Helpers;
using FluentValidation;

namespace CandidateTestTask.Service.Validators.Auth;

public class LoginCreateModelValidator : AbstractValidator<LoginCreateModel>
{
    public LoginCreateModelValidator()
    {
        RuleFor(user => user.Email)
            .Must(ValidationHelper.IsEmailValid);

        RuleFor(user => user.Password)
                .Must(ValidationHelper.IsPasswordHard);
    }
}