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
    public class ContatoPessoaDomain : AbstractValidator<ContatosPessoaEntity>
    {
        ContatoPessoaDomain() 
        {
            RuleFor(cp => cp.Numero)
                .NotEmpty().WithMessage("Informe o número.")
                .NotNull().WithMessage("Informe o número.");

            RuleFor(cp => cp.Regiao.RegiaoCodigoAreas)
                .NotNull().WithMessage("Região e código de ´´area não informados");
        }

        public bool ValidaContato(ValidaContatoPessoa validaContatoPessoa, ContatosPessoaEntity contatosPessoaEntity) 
        {
            return (contatosPessoaEntity.TipoContatoPessoa == 1)? 
                   ValidaContato(new ValidaContatoPessoaCelular(), contatosPessoaEntity):
                   ValidaContato(new ValidaContatoPessoaFixo(), contatosPessoaEntity);         
        }

       
    }
}
