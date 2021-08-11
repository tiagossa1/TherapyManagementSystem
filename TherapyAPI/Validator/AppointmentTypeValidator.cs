using FluentValidation;
using System;
using TherapyAPI.Models;

namespace TherapyAPI.Validators
{
    public class AppointmentTypeValidator : AbstractValidator<AppointmentType>
    {
        public AppointmentTypeValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
