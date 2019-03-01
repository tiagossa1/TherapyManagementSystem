using FluentValidation;
using TherapyAPI.Models;

namespace TherapyAPI.Validators
{
    public class TherapistValidator : AbstractValidator<Therapist>
    {
        public TherapistValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Gender).NotEmpty().NotNull().Must(checkGender);
            RuleFor(x => x.MobileNumber).NotEmpty().NotNull().Length(1, 9);
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
        }

        private bool checkGender(char gender)
        {
            return string.Equals(gender.ToString().ToLower(), "m") || string.Equals(gender.ToString().ToLower(), "f");
        }
    }
}