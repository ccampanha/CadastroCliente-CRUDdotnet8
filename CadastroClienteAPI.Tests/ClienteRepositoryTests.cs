using CadastroClienteAPI.Infrastructure;
using CadastroClienteAPI.Models;
using CadastroClienteAPI.Models.Enum;
using CadastroClienteAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CadastroClienteAPI.Tests
{
    public class ClienteRepositoryTests
    {
        private readonly CadastroClienteContext _context;
        private readonly ClienteRepository _repository;

        public ClienteRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CadastroClienteContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new CadastroClienteContext(options);
            _repository = new ClienteRepository(_context);
        }

        // Método auxiliar para criar um cliente válido
        private Cliente GetValidCliente()
        {
            return new Cliente
            {
                Nome = "Empresa da Cris",
                Cnpj = "12345678901234",
                Status = StatusCliente.Ativo, // Usando o enum StatusCliente
                Enderecos = new List<Endereco>
                {
                    new Endereco
                    {
                        Cep = "22030000",
                        Logradouro = "Tonelero",
                        Numero = 639,
                        Complemento = "apto 1001",
                        Bairro = "Copacabana",
                        Cidade = "Rio de Janeiro",
                        UF = "RJ",
                        Descricao = "dados de teste"
                    }
                },
                Telefones = new List<Telefone>
                {
                    new Telefone
                    {
                        Numero = "988832222",
                        DDD = "21",
                        Tipo = TipoTelefone.Atendimento, // Usando o enum TipoTelefone
                        Contato = "Cris"
                    }
                },
                Emails = new List<Email>
                {
                    new Email
                    {
                        EnderecoEmail = "abc@hotmail.com"
                    }
                }
            };
        }

        // Teste de adicionar cliente
        [Fact]
        public async Task AdicionarCliente_DeveAdicionarCliente()
        {
            // Arrange
            var cliente = GetValidCliente();

            // Act
            await _repository.AddCliente(cliente);
            await _context.SaveChangesAsync();

            // Assert
            var clienteAdicionado = await _context.Clientes.Include(c => c.Enderecos)
                                                           .Include(c => c.Telefones)
                                                           .Include(c => c.Emails)
                                                           .FirstAsync();
            Assert.Equal("Empresa da Cris", clienteAdicionado.Nome);
            Assert.Equal("12345678901234", clienteAdicionado.Cnpj);
            Assert.Single(clienteAdicionado.Enderecos);
            Assert.Single(clienteAdicionado.Telefones);
            Assert.Equal(TipoTelefone.Atendimento, clienteAdicionado.Telefones.First().Tipo); // Verificando o enum TipoTelefone
            Assert.Equal(StatusCliente.Ativo, clienteAdicionado.Status); // Verificando o enum StatusCliente
        }

        // Teste de obter clientes
        [Fact]
        public async Task ObterClientes_DeveRetornarClientes()
        {
            // Arrange
            var cliente1 = GetValidCliente();
            var cliente2 = new Cliente
            {
                Nome = "Outra Empresa",
                Cnpj = "98765432101234",
                Status = StatusCliente.Inativo, // Usando enum StatusCliente
                Enderecos = new List<Endereco>
                {
                    new Endereco
                    {
                        Cep = "23045000",
                        Logradouro = "Paulista",
                        Numero = 1000,
                        Bairro = "Centro",
                        Cidade = "São Paulo",
                        UF = "SP",
                        Descricao = "dados de teste"
                    }
                },
                Telefones = new List<Telefone>
                {
                    new Telefone
                    {
                        Numero = "999991111",
                        DDD = "11",
                        Tipo = TipoTelefone.Comercial, // Usando o enum TipoTelefone
                        Contato = "Empresa"
                    }
                },
                Emails = new List<Email>
                {
                    new Email
                    {
                        EnderecoEmail = "empresa@gmail.com"
                    }
                }
            };

            _context.Clientes.Add(cliente1);
            _context.Clientes.Add(cliente2);
            await _context.SaveChangesAsync();

            // Act
            var clientes = await _repository.GetClientesPaginado(1, 10);

            // Assert
            Assert.Equal(2, clientes.Count());
        }

        // Teste de atualizar cliente
        [Fact]
        public async Task AtualizarCliente_DeveAtualizarCliente()
        {
            // Arrange
            var cliente = GetValidCliente();
            await _repository.AddCliente(cliente);
            await _context.SaveChangesAsync();

            // Act
            cliente.Nome = "Empresa Atualizada";
            await _repository.UpdateCliente(cliente);
            await _context.SaveChangesAsync();

            // Assert
            var clienteAtualizado = await _context.Clientes.FirstAsync();
            Assert.Equal("Empresa Atualizada", clienteAtualizado.Nome);
        }

        // Teste de deletar cliente
        [Fact]
        public async Task DeletarCliente_DeveDeletarCliente()
        {
            // Arrange
            var cliente = GetValidCliente();
            await _repository.AddCliente(cliente);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteCliente(cliente.Id);
            await _context.SaveChangesAsync();

            // Assert
            Assert.Empty(await _context.Clientes.ToListAsync());
        }
    }
}

