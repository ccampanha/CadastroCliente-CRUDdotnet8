using AutoMapper;
using CadastroClienteAPI.Interface;
using CadastroClienteAPI.Models;
using CadastroClienteAPI.Models.DTO;
using CadastroClienteAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CadastroClienteAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class EnderecoController : ControllerBase
    {
        private readonly BuscaEnderecoCepService _buscaEnderecoCepService;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public EnderecoController(BuscaEnderecoCepService buscaEnderecoCepService, IClienteService clienteService, IMapper mapper)
        {
            _buscaEnderecoCepService = buscaEnderecoCepService;
            _clienteService = clienteService;
            _mapper = mapper;
        }

        /// <summary>
        /// Busca um endereço através do CEP utilizando a a API ViaCEP.
        /// </summary>
        /// <param name="cep">Cep do endereço a ser carregado. Inserir apenas valores numéricos.</param>
        /// <returns>Resultado da operação</returns>
        [HttpGet("BuscaEnderecoCEP/{cep}")]
        public async Task<IActionResult> BuscaEnderecoCEP(string cep)
        {
            try
            {
                var endereco = await _buscaEnderecoCepService.BuscarEnderecoPorCep(cep);
                if (endereco == null)
                {
                    return NotFound("Endereço não encontrado para o CEP fornecido.");
                }
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adiciona um novo endereço a um cliente utilizando a API ViaCEP para carregar parte dos dados.
        /// </summary>
        /// <param name="clienteId">ID do cliente a ser atualizado</param>
        /// <param name="cep">Cep do endereço a ser carregado. Inserir apenas valores numéricos.</param>
        /// <param name="numero">Numero da casa, edificio, etc, caso exista</param>
        /// <param name="complemento">Complemento do endereço, como Apaartamento, Bloco, etc</param>
        /// <returns>Resultado da operação</returns>
        [HttpPost("AddEnderecoCep")]
        public async Task<IActionResult> AddEnderecoCep(int clienteId, string cep, int? numero = null, string? complemento = null)
        {
            try
            {
                // Validação do CEP (8 dígitos numéricos)
                if (!Regex.IsMatch(cep, @"^\d{8}$"))
                {
                    return BadRequest("O CEP deve conter 8 dígitos numéricos.");
                }

                // Chama o serviço para buscar o endereço pelo CEP
                var enderecoViaCep = await _buscaEnderecoCepService.BuscaEnderecoPorCepAsync(cep);

                if (enderecoViaCep == null)
                {
                    return NotFound($"Endereço com CEP {cep} não encontrado.");
                }

                // Cria o objeto Endereco
                var endereco = new Endereco
                {
                    Cep = enderecoViaCep.Cep,
                    Logradouro = enderecoViaCep.Logradouro,
                    Bairro = enderecoViaCep.Bairro,
                    Cidade = enderecoViaCep.Localidade,
                    UF = enderecoViaCep.Uf,
                    Numero = numero ?? 0, // Default para 0 caso não seja fornecido
                    Complemento = complemento ?? string.Empty, // Default para string vazia
                    Descricao = null
                };

                // Adiciona o endereço ao cliente (ClienteId)
                var resultado = await _clienteService.AddEnderecoAoCliente(clienteId, endereco);

                if (!resultado)
                {
                    return BadRequest("Erro ao adicionar o endereço ao cliente.");
                }

                return Ok("Endereço adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar o endereço: {ex.Message}");
            }
        }

        /// <summary>
        /// Adiciona um novo endereço a um cliente.
        /// </summary>
        /// <param name="id">ID do cliente a ser atualizado</param>
        /// <returns>Resultado da operação</returns>
        [HttpPut("{id}/endereco")]
        public async Task<IActionResult> AddEnderecoToCliente(int id, [FromBody] Endereco endereco)
        {
            try
            {
                // Recupera o cliente existente pelo ID
                var clienteExistente = await _clienteService.GetClienteById(id);

                if (clienteExistente == null)
                {
                    return NotFound($"Cliente com ID {id} não encontrado.");
                }

                // Mapeia o cliente existente para um ClienteDTO
                var clienteDTO = _mapper.Map<ClienteDTO>(clienteExistente);

                // Converte o endereço recebido para EnderecoDTO
                var enderecoDTO = _mapper.Map<EnderecoDTO>(endereco);

                // Atualiza ou adiciona o endereço no ClienteDTO
                clienteDTO.Enderecos.Add(enderecoDTO); // Certifique-se de que o cliente tenha uma lista de Enderecos

                // Chama o serviço para atualizar o cliente
                await _clienteService.UpdateCliente(id, clienteDTO);

                return Ok("Endereço adicionado e cliente atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar o endereço e atualizar o cliente: {ex.Message}");
            }
        }
    }
}