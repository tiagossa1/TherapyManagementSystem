using FluentValidation;
using TherapyAPI.Models;

namespace TherapyAPI.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.CivilStatusId).NotEmpty();
            RuleFor(x => x.GenderId).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email));
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.MobileNumber).Length(1, 9).When(x => !string.IsNullOrEmpty(x.MobileNumber));
            RuleFor(x => x.NIF).Length(1, 9).When(x => !string.IsNullOrEmpty(x.NIF));
        }
    }
}