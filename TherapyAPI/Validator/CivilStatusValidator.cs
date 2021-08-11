using FluentValidation;
using System;
using TherapyAPI.Models;

namespace TherapyAPI.Validators
{
    public class CivilStatusValidator : AbstractValidator<CivilStatus>
    {
        public CivilStatusValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
