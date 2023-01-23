﻿using ApplicationMediatr.Commands.User;
using FluentValidation;
namespace ApplicationMediatr.Validators.ProjectCharter;

public class UserCreateUpdateValidator : AbstractValidator<UserCreateUpdateCommand>
{
    public UserCreateUpdateValidator()
    {
        RuleFor(prop => prop.FirstName)
            .NotEmpty().WithMessage("{PropertyName} No puede Ser vacio")
            .MaximumLength(100).WithMessage("{PropertyName} no debe exceder {MaxLenght} caracteres");
        RuleFor(prop => prop.LastName)
            .NotEmpty().WithMessage("{PropertyName} No puede Ser vacio")
            .MaximumLength(100).WithMessage("{PropertyName} no debe exceder {MaxLenght} caracteres");
        RuleFor(prop => prop.Username)
            .NotEmpty().WithMessage("{PropertyName} No puede Ser vacio")
            .MaximumLength(25).WithMessage("{PropertyName} no debe exceder {MaxLenght} caracteres");
    }
}
