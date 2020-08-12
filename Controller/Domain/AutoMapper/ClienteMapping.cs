using AutoMapper;
using Domain.DTO;
using Domain.Models;

namespace Domain.AutoMapper
{
    class ClienteMapping : Profile
    {
        public ClienteMapping()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
        }
    }
}
