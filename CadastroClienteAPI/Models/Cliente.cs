namespace CadastroClienteAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using CadastroClienteAPI.Models.Enum;

    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(14)]
        public string Cnpj { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public StatusCliente Status { get; set; } = StatusCliente.Ativo;

        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<Telefone> Telefones { get; set; }
        public ICollection<Email> Emails { get; set; }
    }

}
