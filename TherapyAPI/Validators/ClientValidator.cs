using FluentValidation;
using TherapyAPI.Models;

namespace TherapyAPI.Validators {
    public class ClientValidator : AbstractValidator<Client> {
        public ClientValidator () {
            RuleFor (x => x.CivilStatus).Must (ValidateCivilStatus);
            RuleFor (x => x.Email).NotEmpty ().NotNull ().EmailAddress ();
            RuleFor (x => x.DateOfBirth).NotEmpty ().NotNull ();
            RuleFor (x => x.MobileNumber).Length (1, 9).When (x => !string.IsNullOrEmpty (x.MobileNumber));
            RuleFor (x => x.NIF).Length (1, 9).When (x => !string.IsNullOrEmpty (x.NIF));
        }

        private bool ValidateCivilStatus (char civilStatus) {
            return string.Equals (civilStatus.ToString ().ToLower (), "c") || string.Equals (civilStatus.ToString ().ToLower (), "s") || string.Equals (civilStatus.ToString ().ToLower (), "d") || string.Equals (civilStatus.ToString ().ToLower (), "v") || string.Equals (civilStatus.ToString ().ToLower (), "m") || string.Equals (civilStatus.ToString ().ToLower (), "w");
        }
    }
}