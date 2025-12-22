using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.DTOs;
using FluentValidation;

namespace CoworkingSpaceManager.API.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("The email is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("The password is required");
        }
    }
}