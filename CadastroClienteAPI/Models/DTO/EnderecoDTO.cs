using System.Text.Json.Serialization;

namespace CadastroClienteAPI.Models.DTO
{
    public class EnderecoDTO
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? Cep { get; set; }

        public string Logradouro { get; set; }

        public int? Numero { get; set; } = null;

        public string? Complemento { get; set; } = null;

        public string? Bairro { get; set; } = null;

        public string Cidade { get; set; } = "";

        public string UF { get; set; } = "";

        public string? Descricao { get; set; } = null;
    }
}
