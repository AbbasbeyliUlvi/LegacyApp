using FluentValidation;

namespace LegacyApp.Users;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Firstname).NotEmpty().WithMessage("Please specify a first name");
        RuleFor(x => x.EmailAddress).EmailAddress();
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now.Date);
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now.Date.AddYears(21));
    }
}
