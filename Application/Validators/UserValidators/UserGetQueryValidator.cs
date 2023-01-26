using Application.Commands.User;
using Application.Queries.User;
using FluentValidation;

namespace Application.Validators.ProjectCharter
{
    public class UserGetQueryValidator : AbstractValidator<UserGetQuery>
    {
        public UserGetQueryValidator()
        {
            RuleFor(prop => prop.PageNumber)
                .GreaterThan(0).WithMessage("You Need to specify the {PropertyName} for indicates the page");
            RuleFor(prop => prop.PageSize)
                .GreaterThan(0).WithMessage("You Need to specify the {PropertyName} for indicates the page size");
        }
    }
}
