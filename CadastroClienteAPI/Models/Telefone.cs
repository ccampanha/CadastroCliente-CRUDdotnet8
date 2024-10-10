using CadastroClienteAPI.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace CadastroClienteAPI.Models
{
    public class Telefone
    {
        [Key]
        public int Id { get; set; }

        [StringLength(2)]
        public string DDD { get; set; }

        [StringLength(9)]
        public string Numero { get; set; }

        public TipoTelefone Tipo { get; set; }

        [MaxLength(20)]
        public string? Contato { get; set; }
    }
}
