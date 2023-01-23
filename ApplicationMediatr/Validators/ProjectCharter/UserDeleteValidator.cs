using ApplicationMediatr.Commands.User;
using FluentValidation;

namespace ApplicationMediatr.Validators.ProjectCharter
{
    public class UserDeleteValidator : AbstractValidator<UserDeleteCommand>
    {
        public UserDeleteValidator()
        {
            RuleFor(prop => prop.Id)
                .GreaterThan(0).WithMessage("You Need to specify the {PropertyName} for Deletion");
        }
    }
}
