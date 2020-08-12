using AutoMapper;
using Domain.DTO;
using Domain.Models;

namespace Domain.AutoMapper
{
    public class FilmeMapping : Profile
    {
        public FilmeMapping()
        {
            CreateMap<Filme, FilmeDTO>().ReverseMap();
        }
    }
}
