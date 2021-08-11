using FluentValidation;
using System;
using TherapyAPI.Models;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI.Validators
{
    public class AppointmentValidator : AbstractValidator<Appointment>
    {
        public AppointmentValidator(IAppointmentTypeRepository appointmentTypeRepository, IClientRepository clientRepository, ITherapistRepository therapistRepository)
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.AppointmentType).Custom(async (appointmentType, context) =>
            {
                if (await appointmentTypeRepository.GetById(appointmentType.Id).ConfigureAwait(false) == null)
                    context.AddFailure("Appointment Type is invalid.");
            }).When(x => x.AppointmentType != null);
            RuleFor(x => x.Client).Custom(async (client, context) =>
            {
                if (await clientRepository.GetById(client.Id).ConfigureAwait(false) == null)
                    context.AddFailure("Client is invalid.");
            }).When(x => x.Client != null);
            RuleFor(x => x.Therapist).Custom(async (therapist, context) =>
            {
                if (await therapistRepository.GetById(therapist.Id).ConfigureAwait(false) == null)
                    context.AddFailure("Therapist is invalid.");
            }).When(x => x.Therapist != null);
            RuleFor(x => x.AppointmentDate).Must(x => x != DateTime.MinValue);
        }
    }
}
