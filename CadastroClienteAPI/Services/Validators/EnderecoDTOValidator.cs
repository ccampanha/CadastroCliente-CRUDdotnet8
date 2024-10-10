using CadastroClienteAPI.Models;
using CadastroClienteAPI.Models.DTO;
using FluentValidation;

namespace CadastroClienteAPI.Services.Validators
{
    public class EnderecoDTOValidator : AbstractValidator<EnderecoDTO>
    {
        public EnderecoDTOValidator()
        {
            RuleFor(e => e.Cep)
                .NotEmpty().WithMessage("O CEP é obrigatório.")
                .Matches(@"^\d{8}$").WithMessage("O CEP deve conter 8 dígitos.");

            RuleFor(e => e.Logradouro)
                .NotEmpty().WithMessage("O logradouro é obrigatório.");

            RuleFor(e => e.Numero)
                .NotEmpty().WithMessage("O número é obrigatório.");

            RuleFor(e => e.Bairro)
                .NotEmpty().WithMessage("O bairro é obrigatório.");

            RuleFor(e => e.Cidade)
                .NotEmpty().WithMessage("A cidade é obrigatória.");

            RuleFor(e => e.UF)
                .NotEmpty().WithMessage("A UF é obrigatória.")
                .Length(2).WithMessage("A UF deve ter 2 caracteres.");
        }
    }

}
