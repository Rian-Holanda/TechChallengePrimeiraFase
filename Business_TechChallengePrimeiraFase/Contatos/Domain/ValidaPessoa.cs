using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_TechChallengePrimeiraFase.Entities;

namespace Business_TechChallengePrimeiraFase.Contatos.Domain
{
    public class ValidaPessoa : AbstractValidator<PessoasEntity>
    {
        public ValidaPessoa() 
        {
            RuleFor(p => p.Email)
                .NotNull().WithMessage("Preencha o e-mail")
                .NotEmpty().WithMessage("Preencha o e-mail");

            RuleFor(p => p.Nome)
                .NotNull().WithMessage("Preencha o nome")
                .NotEmpty().WithMessage("Preencha o nome")
                .MaximumLength(200);

        }
    }
}
