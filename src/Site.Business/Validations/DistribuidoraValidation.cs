using FluentValidation;
using Site.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Business.Validations
{
    class DistribuidoraValidation : AbstractValidator<Distribuidora>
    {
        public DistribuidoraValidation()
        {
            RuleFor(d => d.Nome)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("o Tamanho tem que ser de 2 ate 100 Caracteres");

            RuleFor(d => d.Sobre)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
              .Length(2, 1000).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
