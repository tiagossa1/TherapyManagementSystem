using FluentValidation;
using TherapyAPI.Models;

namespace TherapyAPI.Validators
{
    public class TherapistValidator : AbstractValidator<Therapist>
    {
        public TherapistValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.MobileNumber).NotEmpty().NotNull().Length(1, 9);
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
        }
    }
}