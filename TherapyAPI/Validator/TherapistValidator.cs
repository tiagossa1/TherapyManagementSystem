using FluentValidation;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI.Validators
{
    public class TherapistValidator : AbstractValidator<Therapist>
    {
        public TherapistValidator(IGenderRepository genderRepository)
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.MobileNumber).NotEmpty().Length(1, 9);
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email));
            RuleFor(x => x.Gender).Custom(async (gender, context) =>
            {
                if (await genderRepository.GetById(gender.Id).ConfigureAwait(false) == null)
                    context.AddFailure("Gender is invalid.");
            }).When(x => x.Gender != null);
        }
    }
}