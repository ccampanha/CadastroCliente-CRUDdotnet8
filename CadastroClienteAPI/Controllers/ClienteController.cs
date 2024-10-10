using Microsoft.AspNetCore.Mvc;
using CadastroClienteAPI.Interface;
using CadastroClienteAPI.Models.DTO;
using AutoMapper;

[Route("[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly IMapper _mapper;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    /// <summary>
    /// Lista paginada de clientes.
    /// </summary>
    /// <param name="pageNumber">Número da página.</param>
    /// <param name="pageSize">Quantidade de páginas.</param>
    /// <returns>Uma coleção de clientes formatados.</returns>
    [HttpGet]
    public async Task<IActionResult> GetClientesPaginado(int pageNumber = 1, int pageSize = 10)
    {
        var clientes = await _clienteService.GetClientesPaginado(pageNumber, pageSize);
        if (clientes == null || !clientes.Any())
        {
            return NotFound("Nenhum cliente encontrado.");
        }

        var informacoesClientes = clientes.Select(cliente => new
        {
            cliente.Id,
            cliente.Cnpj,
            cliente.Nome,
            cliente.Status,
            Enderecos = cliente.Enderecos.Select(end => new
            {
                end.Id,
                end.Logradouro,
                end.Numero,
                end.Complemento,
                end.Bairro,
                end.Cidade,
                end.UF,
                end.Cep,
                end.Descricao
            }).ToList(),
            Emails = cliente.Emails.Select(email => new
            {
                email.Id,
                email.EnderecoEmail
            }).ToList(),
            Telefones = cliente.Telefones.Select(t => new
            {
                t.Id,
                t.DDD,
                t.Numero,
                t.Tipo,
                t.Contato
            }).ToList()
        });

        return Ok(informacoesClientes);
    }

    /// <summary>
    /// Dados de um cliente pelo seu ID.
    /// </summary>
    /// <param name="id">ID do cliente.</param>
    /// <returns>O cliente encontrado ou null se não existir.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClienteById(int id)
    {
        var cliente = await _clienteService.GetClienteById(id);
        if (cliente == null)
        {
            return NotFound($"Cliente com ID {id} não encontrado.");
        }
        return Ok(cliente);
    }

    /// <summary>
    /// Cria um novo cliente.
    /// </summary>
    /// <param name="clienteDTO">Objeto cliente a ser criado.</param>
    /// <returns>O cliente criado com seu ID atribuído.</returns>
    [HttpPost]
    public async Task<IActionResult> AddCliente([FromBody] ClienteDTO clienteDTO)
    {
        try
        {
            await _clienteService.AddCliente(clienteDTO);
            return CreatedAtAction(nameof(GetClienteById), new { id = clienteDTO.Id }, clienteDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar o cliente: {ex.Message}");
        }
    }


    /// <summary>
    /// Atualiza os dados de um cliente existente.
    /// </summary>
    /// <param name="id">ID do cliente a ser atualizado</param>
    /// <param name="clienteDTO">Dados modificados do cliente</param>
    /// <returns>Resultado da operação</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCliente(int id, [FromBody] ClienteDTO clienteDTO)
    {
        if (clienteDTO == null)
        {
            return BadRequest();
        }

        await _clienteService.UpdateCliente(id, clienteDTO);
        return NoContent();
    }



    /// <summary>
    /// Exclui um cliente pelo seu ID.
    /// </summary>
    /// <param name="id">ID do cliente a ser excluído.</param>
    /// <returns>Task representando a operação assíncrona.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        await _clienteService.DeleteCliente(id);
        return NoContent(); 
    }
}
