using AutoMapper;
using TherapyAPI.Dto;
using TherapyAPI.Models;

namespace TherapyAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<AppointmentType, AppointmentTypeDto>().ReverseMap();
            CreateMap<Billing, BillingDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Therapist, TherapistDto>().ReverseMap();
        }
    }
}