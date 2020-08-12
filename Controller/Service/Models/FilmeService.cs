using AutoMapper;
using Domain.Models;
using Repository.Models;
using System;
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

        public object BuscarTodos()
        {
            return _filmeRepository.GetAll().ToList();
        }

        public object Salvar(Filme filme)
        {
            _filmeRepository.Save(filme);

            return filme;
        }

        public object Atualizar(Filme filme, int id, string titulo)
        {
            _filmeRepository.EncontrarFilme(id, titulo);

            // Verificar se novo titulo está disponivel

            filme.Id = id;

            _filmeRepository.Update(filme);

            return filme;
        }

        public void Deletar(int id, string titulo)
        {
            _filmeRepository.EncontrarFilme(id, titulo);

            // verificar locacoes

            _filmeRepository.Delete(x => x.Id == id);
        }

        public Filme GetById(int id)
        {
            return _filmeRepository.GetById(id);
        }

        public Filme GetByTitulo(string titulo)
        {
            return _filmeRepository.GetByTitulo(titulo);
        }
    }
}
