using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_TechChallengePrimeiraFase.Entities;
using FluentValidation;
using static Business_TechChallengePrimeiraFase.Contatos.Domain.ValidaContatoPessoa;

namespace Business_TechChallengePrimeiraFase.Contatos.Domain
{
    public class ContatoPessoaDomain : AbstractValidator<ContatoPessoaEntity>
    {
        public ContatoPessoaDomain() 
        {
            RuleFor(cp => cp.Numero)
                .NotEmpty().WithMessage("Informe o número.")
                .NotNull().WithMessage("Informe o número.");

            RuleFor(cp => cp.IdRegiao != 0)
                .NotNull().WithMessage("Informe a Sigla");
        }

        public bool ValidaTipoContato(ValidaContatoPessoa validaContatoPessoa, ContatoPessoaEntity ContatoPessoaEntity) 
        {
            return validaContatoPessoa.ValidaContato(ContatoPessoaEntity);         
        }

       
    }
}
