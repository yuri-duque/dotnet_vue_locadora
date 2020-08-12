using AutoMapper;
using Domain.DTO;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Service.Models
{
    public class LocacaoService
    {
        private readonly LocacaoRepository _locacaoRepository;
        private readonly FilmeService _filmeService;
        private readonly ClienteService _clienteService;
        private readonly IMapper _mapper;

        public LocacaoService(LocacaoRepository locacaoRepository, FilmeService filmeService, ClienteService clienteService, IMapper mapper)
        {
            _locacaoRepository = locacaoRepository;
            _filmeService = filmeService;
            _clienteService = clienteService;
            _mapper = mapper;
        }

        public IList<LocacaoDTO> BuscarTodos()
        {
            var list = _locacaoRepository
                .GetAll()
                .Include(x => x.Filme)
                .Include(x => x.Cliente)
                .ToList();

            var listDTO = _mapper.Map<List<LocacaoDTO>>(list);

            return listDTO;
        }

        public Locacao Alugar(LocacaoAlugarDTO locacaoDTO)
        {
            var filme = _filmeService.GetById(locacaoDTO.IdFilme);
            if(filme == null)
                throw new Exception("Filme não encontrado!");

            var cliente = _clienteService.GetById(locacaoDTO.IdFilme);
            if (cliente == null)
                throw new Exception("Cliente não encontrado!");

            var locacao = _mapper.Map<Locacao>(locacaoDTO);
            locacao.DataLocacao = DateTime.Now;

            if(filme.Lancamento)
                locacao.DataDevolucao = DateTime.Now.AddDays(2);
            else
                locacao.DataDevolucao = DateTime.Now.AddDays(3);

            _locacaoRepository.Save(locacao);

            return locacao;
        }

        public Locacao Atualizar(LocacaoAtualizarDTO locacaoDTO, int idCliente, int idFilme)
        {
            _locacaoRepository.EncontrarLocacao(idCliente, idFilme);

            var locacao = _mapper.Map<Locacao>(locacaoDTO);

            _locacaoRepository.Update(locacao);

            return locacao;
        }

        public Locacao Devolver(LocacaoAlugarDTO locacaoDTO)
        {
            var filme = _filmeService.GetById(locacaoDTO.IdFilme);
            if (filme == null)
                throw new Exception("Filme não encontrado!");

            var cliente = _clienteService.GetById(locacaoDTO.IdFilme);
            if (cliente == null)
                throw new Exception("Cliente não encontrado!");

            var locacao = _locacaoRepository.EncontrarLocacao(locacaoDTO.IdCliente, locacaoDTO.IdFilme);
            if(locacao.FilmeDevolvido)
                throw new Exception("Essa locação já foi devolvida!");

            locacao.FilmeDevolvido = true;

            _locacaoRepository.Update(locacao);

            return locacao;
        }

        public void Deletar(int idCliente, int idFilme)
        {
            var locacao = _locacaoRepository.EncontrarLocacao(idCliente, idFilme);

            if (!locacao.FilmeDevolvido)
                throw new Exception("Esse Filme está alugado! Não é possivel excluir ele agora.");

            _locacaoRepository.Delete(x => x.IdCliente == idCliente && x.IdFilme == idFilme);
        }
    }
}
