using AutoMapper;
using BuddhaTerapiasAPI.Dtos;
using BuddhaTerapiasAPI.Models;

namespace BuddhaTerapias_API.Models
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
        }
    }
}