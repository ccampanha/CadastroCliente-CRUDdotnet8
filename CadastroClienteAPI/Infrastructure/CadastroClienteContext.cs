using CadastroClienteAPI.Models;
using CadastroClienteAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace CadastroClienteAPI.Infrastructure
{
    public class CadastroClienteContext(DbContextOptions<CadastroClienteContext> options) : DbContext(options)
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}
