﻿using Business_TechChallengePrimeiraFase.Contatos.Application.Interfaces;
using Entities_TechChallengePrimeiraFase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_TechChallengePrimeiraFase.Contatos.Domain
{
    public abstract class ValidaContatoPessoa
    {
        public abstract IContatoPessoa ValidaContato();

        public bool ValidaContato(ContatoPessoaEntity ContatoPessoaEntity)
        {
            var validaContato = ValidaContato();

            return validaContato.ValidaContato(ContatoPessoaEntity);
        }

        public class ValidaContatoPessoaFixo : ValidaContatoPessoa
        {
            public override IContatoPessoa ValidaContato()
            {
                return new ValidaContatoFixo();
            }

        }

        public class ValidaContatoPessoaCelular : ValidaContatoPessoa
        {
            public override IContatoPessoa ValidaContato()
            {
                return new ValidaContatoCelular();
            }

        }

    }
}
