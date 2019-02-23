using AutoMapper;
using TherapyAPI.Dto;
using TherapyAPI.Models;

namespace TherapyAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Therapist, TherapistDto>();
            CreateMap<TherapistDto, Therapist>();
            CreateMap<Appointment, AppointmentDto>();
        }
    }
}