namespace CadastroClienteAPI.Services.Validators
{
    using CadastroClienteAPI.Models.DTO;
    using FluentValidation;

    public class ClienteDTOValidator : AbstractValidator<ClienteDTO>
    {
        public ClienteDTOValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 caracteres.");

            RuleFor(c => c.Cnpj)
                .NotEmpty().WithMessage("O CNPJ é obrigatório.")
                .Matches(@"^\d{14}$").WithMessage("O CNPJ deve conter 14 dígitos.");

            RuleFor(c => c.Status)
                .NotNull().WithMessage("O status do cliente é obrigatório.");

            // Validação dos Endereços
            RuleForEach(c => c.Enderecos).SetValidator(new EnderecoDTOValidator());

            // Validação dos Emails
            RuleForEach(c => c.Emails).SetValidator(new EmailDTOValidator());

            // Validação dos Telefones
            RuleForEach(c => c.Telefones).SetValidator(new TelefoneDTOValidator());
        }
    }


}
