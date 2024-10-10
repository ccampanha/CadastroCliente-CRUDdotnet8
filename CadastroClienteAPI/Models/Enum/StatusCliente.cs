using System.ComponentModel;

namespace CadastroClienteAPI.Models.Enum
{
    public enum StatusCliente
    {
        [Description("Ativo")]
        Ativo = 1,
        [Description("Inativo")]
        Inativo = 0,
    }
}
