using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.DTOs;
using FluentValidation;

namespace CoworkingSpaceManager.API.Validators
{
    public class SpacePostValidator : AbstractValidator<SpacePostDto>
    {
        public SpacePostValidator()
        {
            RuleFor(x => x.Capacity).GreaterThan(0).WithMessage("The capacity have to be greater than 0");
            RuleFor(x => x.Name).NotEmpty().WithMessage("The name field is required");
        }
    }
}