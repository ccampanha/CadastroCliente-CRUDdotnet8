using CadastroClienteAPI.Interface;
using CadastroClienteAPI.Models;

namespace CadastroClienteAPI.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using CadastroClienteAPI.Interface;
    using CadastroClienteAPI.Models;
    using CadastroClienteAPI.Models.DTO;

    public class ClienteService : IClienteService
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IMapper mapper, IClienteRepository clienteRepository)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteDTO> GetClienteById(int id)
        {
            var cliente = await _clienteRepository.GetClienteById(id); 
            if (cliente == null)
            {
                return null; 
            }

            return _mapper.Map<ClienteDTO>(cliente); 
        }
    


        public async Task<IEnumerable<ClienteDTO>> GetClientesPaginado(int page, int pageSize)
        {
            var clientes = await _clienteRepository.GetClientesPaginado(page,pageSize);

            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
        }

        public async Task<bool> AddCliente(ClienteDTO clienteDTO)
        {
            {
                var cliente = _mapper.Map<Cliente>(clienteDTO);
                
                return await _clienteRepository.AddCliente(cliente);
            }
        }


        public async Task<bool> UpdateCliente(int clienteId, ClienteDTO clienteDTO)
        {
            var clienteExistente = await _clienteRepository.GetClienteById(clienteId);
            if (clienteExistente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            // Atualizar as propriedades do cliente (exceto a chave primária)
            _mapper.Map(clienteDTO, clienteExistente);

            return await _clienteRepository.UpdateCliente(clienteExistente);
        }


        public async Task DeleteCliente(int id)
        {
            var cliente = await _clienteRepository.GetClienteById(id);
            if (cliente == null) throw new Exception("Cliente não encontrado");

            await _clienteRepository.DeleteCliente(id);
        }

        public async Task<bool> AddEnderecoAoCliente(int clienteId, Endereco endereco)
        {
            var cliente = await _clienteRepository.GetClienteById(clienteId);

            if (cliente == null)
            {
                return false;
            }

            // Adiciona o endereço ao cliente
            cliente.Enderecos.Add(endereco);

            // Salva as alterações
            await _clienteRepository.UpdateCliente(cliente);

            return true;
        }


    }

}
