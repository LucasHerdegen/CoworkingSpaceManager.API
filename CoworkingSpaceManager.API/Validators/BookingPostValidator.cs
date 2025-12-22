using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoworkingSpaceManager.API.DTOs;
using FluentValidation;

namespace CoworkingSpaceManager.API.Validators
{
    public class BookingPostValidator : AbstractValidator<BookingPostDto>
    {
        public BookingPostValidator()
        {
            RuleFor(x => x.SpaceId).GreaterThan(0).WithMessage("The id have to be greater than 0");
            RuleFor(x => x.ReservationDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("No puedes reservar en el pasado.");
        }
    }
}