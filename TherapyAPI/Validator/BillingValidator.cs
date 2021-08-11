using FluentValidation;
using System;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI.Validators
{
    public class BillingValidator : AbstractValidator<Billing>
    {
        public BillingValidator(IAppointmentRepository appointmentRepository)
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Appointment).Custom(async (appointment, context) =>
            {
                if (await appointmentRepository.GetById(appointment.Id).ConfigureAwait(false) == null)
                    context.AddFailure("Appointment is invalid.");
            }).When(x => x.Appointment != null);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.OriginalPrice).GreaterThanOrEqualTo(0).When(x => x.OriginalPrice.HasValue);
        }
    }
}
