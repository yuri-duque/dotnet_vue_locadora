using AutoMapper;
using Domain.DTO;
using Domain.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Models
{
    public class FilmeService
    {
        private readonly FilmeRepository _filmeRepository;
        private readonly LocacaoRepository _locacaoRepository;
        private readonly IMapper _mapper;

        public FilmeService(FilmeRepository filmeRepository, LocacaoRepository locacaoRepository, IMapper mapper)
        {
            _filmeRepository = filmeRepository;
            _locacaoRepository = locacaoRepository;
            _mapper = mapper;
        }

        public IList<FilmeDTO> BuscarTodos()
        {
            var list = _filmeRepository.GetAll().ToList();

            var listDTO = _mapper.Map<List<FilmeDTO>>(list);

            return listDTO;
        }

        public FilmeDTO Salvar(FilmeDTO filmeDTO)
        {
            var filme = _filmeRepository.GetByTitulo(filmeDTO.Titulo);
            if (filme != null)
                throw new Exception("Já existe um filme cadastrado com esse titulo!");

            filme = _mapper.Map<Filme>(filmeDTO);

            _filmeRepository.Save(filme);

            return _mapper.Map<FilmeDTO>(filme);
        }

        public FilmeDTO Atualizar(FilmeDTO filmeDTO, int id)
        {
            var filme = _filmeRepository.EncontrarFilme(id);

            if (!filme.Titulo.Equals(filmeDTO.Titulo))
            {
                filme = _filmeRepository.GetByTitulo(filmeDTO.Titulo);
                if (filme != null)
                    throw new Exception("Já existe um filme cadastrado com esse titulo!");
            }

            filme = _mapper.Map<Filme>(filmeDTO);
            filme.Id = id;

            _filmeRepository.Update(filme);

            return _mapper.Map<FilmeDTO>(filme);
        }

        public void Deletar(int id)
        {
            _filmeRepository.EncontrarFilme(id);

            var Locaoes = _locacaoRepository.GetLocacoesAtivasByFilme(id);
            if (Locaoes != null && Locaoes.Any())
                throw new Exception($"Não é possivel excluir esse filme, pois existem '{Locaoes.Count()}' locações que não foram devolvidas!");

            _filmeRepository.Delete(x => x.Id == id);
        }

        public dynamic GetOptionsSelect()
        {
            var list = _filmeRepository.GetAll().Select(x => new { x.Id, x.Titulo }).ToList();

            return list;
        }

        public IList<FilmeDTO> Relatorio(bool isNuncaAlugados, bool? maisAlugados = null, DateTime? periodoMaisAlugados = null, int? quantidadeFilmes = null)
        {
            var list = _filmeRepository.Relatorio(isNuncaAlugados, maisAlugados, periodoMaisAlugados, quantidadeFilmes).ToList();

            return _mapper.Map<List<FilmeDTO>>(list);
        }
    }
}
