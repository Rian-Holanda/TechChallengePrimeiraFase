using Entities_TechChallengePrimeiraFase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Business_TechChallengePrimeiraFase.Contatos.Application.Interfaces
{
    public interface IContatoPessoa
    {
        bool ValidaContato(ContatoPessoaEntity ContatoPessoaEntity);
    }

    class ValidaContatoCelular: IContatoPessoa 
    {
        public bool ValidaContato(ContatoPessoaEntity ContatoPessoaEntity) 
        {
            try
            {
                bool validacao = false;

                if(ContatoPessoaEntity is { Numero.Length: 11, IdRegiao: > 0 } 
                    && ContatoPessoaEntity.Numero.Substring(2,1) == "9") 
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
        public bool ValidaContato(ContatoPessoaEntity ContatoPessoaEntity) 
        {
            try 
            {
                return ContatoPessoaEntity is { Numero.Length: 10, IdRegiao:  > 0 };
            }
            catch 
            { 
                return false ;
            }
        }
    }
}
