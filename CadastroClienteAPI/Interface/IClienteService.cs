using CadastroClienteAPI.Models;

namespace CadastroClienteAPI.Interface
{
    using CadastroClienteAPI.Models.DTO;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetClientesPaginado(int pageNumber, int pageSize);

        Task<ClienteDTO> GetClienteById(int id);

        Task<bool> AddCliente(ClienteDTO clienteDTO);

        Task<bool> UpdateCliente(int id, ClienteDTO clienteDto);

        Task DeleteCliente(int id);

        Task<bool> AddEnderecoAoCliente(int clienteId, Endereco endereco);
    }

}
