namespace CadastroClienteAPI.Models.Enum
{
    using System.ComponentModel;

    public enum TipoTelefone
    {
        [Description("Atendimento")]
        Atendimento = 1,

        [Description("Comercial")]
        Comercial = 2,

        [Description("Diretoria")]
        Diretoria = 3,

        [Description("Vendas")]
        Vendas = 4,

        [Description("Compras")]
        Compras = 5,

        [Description("Residencial")]
        Residencial = 6
    }
}
