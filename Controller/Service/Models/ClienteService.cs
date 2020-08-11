using AutoMapper;
using Domain.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Models
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(ClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public IList<Cliente> BuscarTodos()
        {
            return _clienteRepository.GetAll().ToList();
        }

        public object Salvar(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public object Atualizar(Cliente cliente, int id)
        {
            throw new NotImplementedException();
        }

        public object Deletar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
