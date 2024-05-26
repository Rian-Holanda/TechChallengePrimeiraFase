using Entities_TechChallengePrimeiraFase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Business_TechChallengePrimeiraFase.Contatos.Applicacation.Interfaces
{
    public interface IContatoPessoa
    {
        bool ValidaContato(ContatosPessoaEntity contatosPessoaEntity);
    }

    class ValidaContatoCelular: IContatoPessoa 
    {
        public bool ValidaContato(ContatosPessoaEntity contatosPessoaEntity) 
        {
            try
            {
                bool validacao = false;

                if(contatosPessoaEntity is { Numero.Length: 11, IdRegiao: > 0 } 
                    && contatosPessoaEntity.Numero.Substring(2,1) == "9") 
                {
                    validacao = true;
                }

                return validacao;
            }
            catch 
            { 
                return false;
            }
        }
    }

    class ValidaContatoFixo : IContatoPessoa
    {
        public bool ValidaContato(ContatosPessoaEntity contatosPessoaEntity) 
        {
            try 
            {
                return contatosPessoaEntity is { Numero.Length: 10, IdRegiao:  > 0 };
            }
            catch 
            { 
                return false ;
            }
        }
    }
}
