using AutoMapper;
using Domain.Models;
using Repository.Models;
using System;

namespace Service.Models
{
    public class LocacaoService
    {
        private readonly LocacaoRepository _locacaoRepository;
        private readonly IMapper _mapper;

        public LocacaoService(LocacaoRepository locacaoRepository, IMapper mapper)
        {
            _locacaoRepository = locacaoRepository;
            _mapper = mapper;
        }

        public object BuscarTodos()
        {
            throw new NotImplementedException();
        }

        public object Salvar(Locacao locacao)
        {
            throw new NotImplementedException();
        }

        public object Atualizar(Locacao locacao, int id)
        {
            throw new NotImplementedException();
        }

        public object Deletar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
