﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_TechChallengePrimeiraFase.Contatos.Applicacation.Interfaces
{
    public interface IValidaEmailPessoa
    {
        public bool ValidaEmail(string? email);

    }
}