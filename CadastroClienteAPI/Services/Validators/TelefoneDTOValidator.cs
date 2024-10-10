using CadastroClienteAPI.Models;
using CadastroClienteAPI.Models.DTO;
using FluentValidation;

namespace CadastroClienteAPI.Services.Validators
{
    public class TelefoneDTOValidator : AbstractValidator<TelefoneDTO>
    {
        public TelefoneDTOValidator()
        {
            RuleFor(t => t.DDD)
                .NotEmpty().WithMessage("O DDD é obrigatório.")
                .Length(2).WithMessage("O DDD deve ter 2 dígitos.");

            RuleFor(t => t.Numero)
                .NotEmpty().WithMessage("O número é obrigatório.")
                .Matches(@"^\d{8,9}$").WithMessage("O número deve ter entre 8 e 9 dígitos.");

            RuleFor(t => t.Tipo)
                .NotEmpty().WithMessage("O tipo de telefone é obrigatório.");

            RuleFor(t => t.Contato)
                .NotEmpty().WithMessage("O contato é obrigatório.");
        }
    }

}
