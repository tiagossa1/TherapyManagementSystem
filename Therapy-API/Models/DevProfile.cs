using AutoMapper;
using BuddhaTerapiasAPI.Dtos;
using BuddhaTerapiasAPI.Models;

namespace BuddhaTerapias_API.Models
{
    public class DevProfile : Profile
    {
        public DevProfile()
        {
            CreateMap<Dev, DevDto>().ReverseMap();
        }
    }
}