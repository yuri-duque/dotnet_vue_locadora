using AutoMapper;
using Domain.DTO;
using Domain.Models;

namespace Domain.AutoMapper
{
    public class LocacaoMapping : Profile
    {
        public LocacaoMapping()
        {
            CreateMap<Locacao, LocacaoDTO>().ReverseMap();
            CreateMap<Locacao, LocacaoAlugarDTO>().ReverseMap();
            CreateMap<Locacao, LocacaoAtualizarDTO>().ReverseMap();
        }
    }
}
