using AutoMapper;
using Domain.DTO;
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

        public IList<ClienteDTO> BuscarTodos()
        {
            var list = _clienteRepository.GetAll().ToList();

            var listDTO = _mapper.Map<List<ClienteDTO>>(list);

            return listDTO;
        }

        public Cliente Salvar(ClienteDTO clienteDTO)
        {
            var cliente = _clienteRepository.GetByCPF(clienteDTO.CPF);
            if (cliente != null)
                throw new Exception("Já existe um cliente cadastrado com esse CPF!");

            cliente = _mapper.Map<Cliente>(clienteDTO);

            _clienteRepository.Save(cliente);

            return cliente;
        }

        public object Atualizar(ClienteDTO clienteDTO, int id)
        {
            _clienteRepository.EncontrarCliente(id, clienteDTO.CPF);

            var cliente = _mapper.Map<Cliente>(clienteDTO);
            cliente.Id = id;

            _clienteRepository.Update(cliente);

            return cliente;
        }

        public void Deletar(int id, string CPF)
        {
            _clienteRepository.EncontrarCliente(id, CPF);

            // verificar locacoes
            var cliente = _clienteRepository.GetLocacoesAtivasByCliente(id);
            if (cliente.Locacoes.Any())
                throw new Exception($"Não é possivel excluir esse cliente, pois existem '{cliente.Locacoes.Count()}' locações que não foram devolvidas!");

            _clienteRepository.Delete(x => x.Id == id);
        }
    }
}
