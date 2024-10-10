using CadastroClienteAPI.Models.Enum;
using System.Text.Json.Serialization;

namespace CadastroClienteAPI.Models.DTO
{
    public class TelefoneDTO
    {
        [JsonIgnore]
        public int? Id { get; set; }

        public string DDD { get; set; }
        public string Numero { get; set; }
        public TipoTelefone Tipo { get; set; }
        public string Contato { get; set; }
    }
}
