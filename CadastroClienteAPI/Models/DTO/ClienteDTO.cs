using CadastroClienteAPI.Models.Enum;
using System.Text.Json.Serialization;

namespace CadastroClienteAPI.Models.DTO
{
    public class ClienteDTO
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Id { get; set; }

        public string Cnpj { get; set; }

        public string Nome { get; set; }

        public StatusCliente Status { get; set; } = StatusCliente.Ativo;

        public ICollection<EnderecoDTO> Enderecos { get; set; } = new List<EnderecoDTO>();
        public ICollection<TelefoneDTO> Telefones { get; set; } = new List<TelefoneDTO>();
        public ICollection<EmailDTO> Emails { get; set; } = new List<EmailDTO>();
    }
}
