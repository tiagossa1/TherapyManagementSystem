using AutoMapper;
using BuddhaTerapiasAPI.Dtos;
using BuddhaTerapiasAPI.Models;

namespace BuddhaTerapias_API.Models
{
    public class TherapistProfile : Profile
    {
        public TherapistProfile()
        {
            CreateMap<Therapist, TherapistDto>().ReverseMap();
        }
    }
}