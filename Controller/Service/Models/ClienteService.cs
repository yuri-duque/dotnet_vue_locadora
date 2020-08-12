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

        public Cliente Salvar(Cliente cliente)
        {
            _clienteRepository.Save(cliente);

            return cliente;
        }

        public object Atualizar(Cliente cliente, int id)
        {
            _clienteRepository.EncontrarCliente(id, cliente.CPF);

            cliente.Id = id;

            _clienteRepository.Update(cliente);

            return cliente;
        }

        public void Deletar(int id, string CPF)
        {
            _clienteRepository.EncontrarCliente(id, CPF);

            // verificar locacoes

            _clienteRepository.Delete(x => x.Id == id);
        }        
    }
}
