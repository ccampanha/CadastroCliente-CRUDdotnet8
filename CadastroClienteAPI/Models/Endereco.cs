using System.ComponentModel.DataAnnotations;

namespace CadastroClienteAPI.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [StringLength(9)]
        public string? Cep { get; set; }

        [StringLength(100)]
        public string Logradouro { get; set; }

        public int? Numero { get; set; } = null;

        [StringLength(50)]
        public string? Complemento { get; set; } = null;

        [Required]
        [StringLength(50)]
        public string? Bairro { get; set; } = null;

        [Required]
        [StringLength(50)]
        public string Cidade { get; set; } = "";

        [Required]
        [StringLength(2)]
        public string UF { get; set; } = "";

        [StringLength(100)]
        public string? Descricao { get; set; } = null;
    }
}
