using AutoMapper;
using Domain.DTO;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Models
{
    public class LocacaoService
    {
        private readonly LocacaoRepository _locacaoRepository;
        private readonly FilmeRepository _filmeRepository;
        private readonly ClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public LocacaoService(LocacaoRepository locacaoRepository, FilmeRepository filmeRepository, ClienteRepository clienteRepository, IMapper mapper)
        {
            _locacaoRepository = locacaoRepository;
            _filmeRepository = filmeRepository;
            _clienteRepository = clienteRepository;
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

        public LocacaoDTO Alugar(LocacaoAlugarDTO locacaoDTO)
        {
            var filme = _filmeRepository.Find(locacaoDTO.IdFilme);
            if(filme == null)
                throw new Exception("Filme não encontrado!");

            var cliente = _clienteRepository.Find(locacaoDTO.IdCliente);
            if (cliente == null)
                throw new Exception("Cliente não encontrado!");

            if (!CompararIdadeComClassificacao(cliente.DataNascimento, filme.ClassificacaoIndicativa))
                throw new Exception("Este filme não é indicado para a fixa etária do cliente!");

            var locacao = _mapper.Map<Locacao>(locacaoDTO);
            locacao.DataLocacao = DateTime.Now;

            if(filme.Lancamento)
                locacao.DataDevolucao = DateTime.Now.AddDays(2);
            else
                locacao.DataDevolucao = DateTime.Now.AddDays(3);

            _locacaoRepository.Save(locacao);

            locacao.Cliente = cliente;
            locacao.Filme = filme;

            return _mapper.Map<LocacaoDTO>(locacao);
        }

        public LocacaoDTO Atualizar(LocacaoAtualizarDTO locacaoDTO, int id)
        {
            _locacaoRepository.EncontrarLocacao(id);

            var locacao = _mapper.Map<Locacao>(locacaoDTO);
            locacao.Id = id;

            _locacaoRepository.Update(locacao);

            locacao.Filme = _filmeRepository.Find(locacaoDTO.IdFilme);
            locacao.Cliente = _clienteRepository.Find(locacaoDTO.IdFilme);

            return _mapper.Map<LocacaoDTO>(locacao);
        }

        public LocacaoDTO Devolver(LocacaoAlugarDTO locacaoDTO)
        {
            var filme = _filmeRepository.Find(locacaoDTO.IdFilme);
            if (filme == null)
                throw new Exception("Filme não encontrado!");

            var cliente = _clienteRepository.Find(locacaoDTO.IdFilme);
            if (cliente == null)
                throw new Exception("Cliente não encontrado!");

            var locacao = _locacaoRepository.EncontrarLocacao(locacaoDTO.Id);
            if (locacao.FilmeDevolvido)
                throw new Exception("Essa locação já foi devolvida!");

            locacao.FilmeDevolvido = true;

            _locacaoRepository.Update(locacao);

            locacao.Cliente = cliente;
            locacao.Filme = filme;

            return _mapper.Map<LocacaoDTO>(locacao);
        }

        public void Deletar(int id)
        {
            var locacao = _locacaoRepository.EncontrarLocacao(id);

            if (!locacao.FilmeDevolvido)
                throw new Exception("Não doi possivel excluir essa locação, pois ela ainda não foi devolvida.");

            _locacaoRepository.Delete(x => x.Id == id);
        }

        private bool CompararIdadeComClassificacao(DateTime dataNascimento, EnumClassificacaoIndicativa classificacao)
        {
            int idade = DateTime.Now.Year - dataNascimento.Year;
            if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
            {
                idade--;
            }

            int classificacaoInt = Convert.ToInt32(classificacao);

            if (idade >= classificacaoInt)
                return true;

            return false;
        }
    }
}
