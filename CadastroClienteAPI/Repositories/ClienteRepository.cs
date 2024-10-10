namespace CadastroClienteAPI.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CadastroClienteAPI.Infrastructure;
    using CadastroClienteAPI.Interface;
    using CadastroClienteAPI.Models;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class ClienteRepository : IClienteRepository
    {
        private readonly CadastroClienteContext _context;

        public ClienteRepository(CadastroClienteContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientesPaginado(int pageNumber, int pageSize)
        {
            return await _context.Clientes
            .Include(c => c.Enderecos)
            .Include(c => c.Emails)
            .Include(c => c.Telefones)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            return await _context.Clientes.Include(c => c.Enderecos)
                                          .Include(c => c.Telefones)
                                          .Include(c => c.Emails)
                                          .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> AddCliente(Cliente cliente)
        {
            try 
            {
                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();
                return true;
            }
            catch 
            { 
                return false;
            }
            
        }

        public async Task<bool> UpdateCliente(Cliente cliente)
        {
            try
            {
                //_context.Entry(cliente).State = EntityState.Modified;
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public async Task DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}

