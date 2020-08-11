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
            VerificarExistenciaCliente(id);

            cliente.Id = id;

            _clienteRepository.Update(cliente);

            return cliente;
        }

        public void Deletar(int id)
        {
            VerificarExistenciaCliente(id);

            // verificar locacoes

            _clienteRepository.Delete(x => x.Id == id);
        }

        private bool VerificarExistenciaCliente(int id)
        {
            var clienteBase = _clienteRepository.Find(id);

            if (clienteBase != null)
                throw new Exception("Cliente não encontrado");

            return true;
        }
    }
}
