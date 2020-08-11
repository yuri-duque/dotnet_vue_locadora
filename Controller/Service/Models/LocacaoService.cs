using AutoMapper;
using Domain.Models;
using Repository.Models;
using System;
using System.Linq;

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
            return _locacaoRepository.GetAll().ToList();
        }

        public object Alugar(Locacao locacao)
        {
            // verificar se o filme não está alugado

            locacao.DataLocacao = DateTime.Now;

            _locacaoRepository.Save(locacao);

            return locacao;
        }

        public object Devolver(Locacao locacao, int idCliente, int idFilme)
        {
            _locacaoRepository.VerificarExistencia("Locação não encontrada", idCliente, idFilme);

            // verificar se o filme já não foi devolvido

            locacao.IdFilme = idFilme;
            locacao.IdCliente = idCliente;
            locacao.DataLocacao = DateTime.Now;

            _locacaoRepository.Update(locacao);

            return locacao;
        }

        public void Deletar(int idCliente, int idFilme)
        {
            _locacaoRepository.VerificarExistencia("Locação não encontrada", idCliente, idFilme);

            // verificar se o filme não está alugado

            _locacaoRepository.Delete(x => x.IdCliente == idCliente && x.IdFilme == idFilme);
        }
    }
}
