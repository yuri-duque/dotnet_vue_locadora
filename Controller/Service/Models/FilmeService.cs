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
        private readonly IMapper _mapper;

        public FilmeService(FilmeRepository filmeRepository, IMapper mapper)
        {
            _filmeRepository = filmeRepository;
            _mapper = mapper;
        }

        public IList<FilmeDTO> BuscarTodos()
        {
            var list = _filmeRepository.GetAll().ToList();

            var listDTO = _mapper.Map<List<FilmeDTO>>(list);

            return listDTO;
        }

        public object Salvar(FilmeDTO filmeDTO)
        {
            var filme = _filmeRepository.GetByTitulo(filmeDTO.Titulo);
            if (filme != null)
                throw new Exception("Já existe um filme cadastrado com esse titulo!");

            filme = _mapper.Map<Filme>(filmeDTO);

            _filmeRepository.Save(filme);

            return filme;
        }

        public object Atualizar(FilmeDTO filmeDTO, int id, string titulo)
        {
            _filmeRepository.EncontrarFilme(id, titulo);

            if (!titulo.Equals(filmeDTO.Titulo))
            {
                var filmeBase = _filmeRepository.GetByTitulo(filmeDTO.Titulo);
                if (filmeBase != null)
                    throw new Exception("Já existe um filme cadastrado com esse titulo!");
            }            

            var filme = _mapper.Map<Filme>(filmeDTO);
            filme.Id = id;

            _filmeRepository.Update(filme);

            return filme;
        }

        public void Deletar(int id, string titulo)
        {
            _filmeRepository.EncontrarFilme(id, titulo);

            var filme = _filmeRepository.GetLocacoesAtivasByIdFilme(id);
            if(filme.Locacoes.Any())
                throw new Exception($"Não é possivel excluir esse filme, pois existem '{filme.Locacoes.Count()}' locações que não foram devolvidas!");

            _filmeRepository.Delete(x => x.Id == id);
        }
    }
}
