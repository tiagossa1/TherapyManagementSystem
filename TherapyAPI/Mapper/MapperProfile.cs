using AutoMapper;
using TherapyAPI.Dto;
using TherapyAPI.Models;

namespace TherapyAPI.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Appointment, AppointmentDto>()
                .ReverseMap()
                .ForMember(x => x.AppointmentTypeId, opt => opt.MapFrom(x => x.AppointmentType.Id))
                .ForMember(x => x.TherapistId, opt => opt.MapFrom(x => x.Therapist.Id))
                .ForMember(x => x.ClientId, opt => opt.MapFrom(x => x.Client.Id));
            CreateMap<AppointmentType, AppointmentTypeDto>().ReverseMap();
            CreateMap<Billing, BillingDto>()
                .ReverseMap()
                .ForMember(x => x.AppointmentId, opt => opt.MapFrom(x => x.Appointment.Id));
            CreateMap<Client, ClientDto>()
                .ReverseMap()
                .ForMember(x => x.CivilStatusId, opt => opt.MapFrom(x => x.CivilStatus.Id))
                .ForMember(x => x.GenderId, opt => opt.MapFrom(x => x.Gender.Id));
            CreateMap<Therapist, TherapistDto>()
                .ReverseMap()
                .ForMember(x => x.GenderId, opt => opt.MapFrom(x => x.Gender.Id));
            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<CivilStatus, CivilStatusDto>().ReverseMap();
        }
    }
}