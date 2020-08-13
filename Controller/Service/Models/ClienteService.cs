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
        private readonly LocacaoRepository _locacaoRepository;
        private readonly IMapper _mapper;

        public ClienteService(ClienteRepository clienteRepository, LocacaoRepository locacaoRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _locacaoRepository = locacaoRepository;
            _mapper = mapper;
        }

        public IList<ClienteDTO> BuscarTodos()
        {
            var list = _clienteRepository.GetAll().ToList();

            var listDTO = _mapper.Map<List<ClienteDTO>>(list);

            return listDTO;
        }

        public ClienteDTO Salvar(ClienteDTO clienteDTO)
        {
            var cliente = _clienteRepository.GetByCPF(clienteDTO.CPF);
            if (cliente != null)
                throw new Exception("Já existe um cliente cadastrado com esse CPF!");

            cliente = _mapper.Map<Cliente>(clienteDTO);

            _clienteRepository.Save(cliente);

            return _mapper.Map<ClienteDTO>(cliente);
        }

        public ClienteDTO Atualizar(ClienteDTO clienteDTO, int id)
        {
            var cliente = _clienteRepository.EncontrarCliente(id);

            if (!cliente.CPF.Equals(clienteDTO.CPF))
            {
                cliente = _clienteRepository.GetByCPF(clienteDTO.CPF);
                if (cliente != null)
                    throw new Exception("Já existe um cliente cadastrado com esse CPF!");
            }

            cliente = _mapper.Map<Cliente>(clienteDTO);
            cliente.Id = id;

            _clienteRepository.Update(cliente);

            return _mapper.Map<ClienteDTO>(cliente);
        }

        public void Deletar(int id)
        {
            _clienteRepository.EncontrarCliente(id);

            var locacoes = _locacaoRepository.GetLocacoesAtivasByCliente(id);
            if (locacoes != null && locacoes.Any())
                throw new Exception($"Não é possivel excluir esse cliente, pois existem '{locacoes.Count()}' locações que não foram devolvidas!");

            _clienteRepository.Delete(x => x.Id == id);
        }

        public dynamic GetOptionsSelect()
        {
            var list = _clienteRepository.GetAll().Select(x => new { x.Id, x.Nome}).ToList();

            return list;
        }

        public IList<ClienteDTO> Relatorio(bool isAtrasados, int? indexRecordistas)
        {
            var list = _clienteRepository.Relatorio(isAtrasados, indexRecordistas).ToList();

            if(indexRecordistas != null && indexRecordistas > 0)
            {
                if (Convert.ToInt32(indexRecordistas) > list.Count())
                    throw new Exception("O número de clientes recordistas é menor que o index informado!");

                var listRecordistas = new List<Cliente>();
                listRecordistas.Add(list[Convert.ToInt32(indexRecordistas) - 1]);

                return _mapper.Map<List<ClienteDTO>>(listRecordistas);
            }

            return _mapper.Map<List<ClienteDTO>>(list);
        }
    }
}
