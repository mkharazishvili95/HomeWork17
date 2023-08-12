﻿using FluentValidation;
using HomeWork17.Models;

namespace HomeWork17.Validation
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator() {
            RuleFor(address => address.Country).NotEmpty().WithMessage("Country must not be empty!");
            RuleFor(address => address.City).NotEmpty().WithMessage("City must not be empty!");
            RuleFor(address => address.HomeNumber).NotEmpty().WithMessage("HomeNumber must not be empty!");
        }
    }
}
