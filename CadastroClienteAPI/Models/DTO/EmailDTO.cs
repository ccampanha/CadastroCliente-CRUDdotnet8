using System.Text.Json.Serialization;

namespace CadastroClienteAPI.Models.DTO
{
    public class EmailDTO
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string EnderecoEmail { get; set; }
    }
}
