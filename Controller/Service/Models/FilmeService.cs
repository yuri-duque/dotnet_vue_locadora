using AutoMapper;
using Domain.Models;
using Repository.Models;
using System;

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
            throw new NotImplementedException();
        }

        public object Salvar(Filme filme)
        {
            throw new NotImplementedException();
        }

        public object Atualizar(Filme filme, int id)
        {
            throw new NotImplementedException();
        }

        public object Deletar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
