using CadastroClienteAPI.Models;

namespace CadastroClienteAPI.Interface
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetClientesPaginado(int pageNumber, int pageSize);
        Task<Cliente> GetClienteById(int id);
        Task<bool> AddCliente(Cliente cliente);
        Task<bool> UpdateCliente(Cliente cliente);
        Task DeleteCliente(int id);
    }
}
