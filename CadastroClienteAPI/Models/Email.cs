using System.ComponentModel.DataAnnotations;

namespace CadastroClienteAPI.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }

        [StringLength(150)]
        public string EnderecoEmail { get; set; }

    }
}
