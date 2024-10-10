using CadastroClienteAPI.Models;
using CadastroClienteAPI.Models.DTO;
using FluentValidation;

namespace CadastroClienteAPI.Services.Validators
{
    public class EmailDTOValidator : AbstractValidator<EmailDTO>
    {
        public EmailDTOValidator()
        {
            RuleFor(e => e.EnderecoEmail)
                .NotEmpty().WithMessage("O endereço de email é obrigatório.")
                .EmailAddress().WithMessage("O endereço de email deve ser válido.");
        }
    }

}
