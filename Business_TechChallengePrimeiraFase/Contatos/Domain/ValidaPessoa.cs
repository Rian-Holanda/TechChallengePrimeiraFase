using Business_TechChallengePrimeiraFase.Contatos.Applicacation.Interfaces;
using Entities_TechChallengePrimeiraFase.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business_TechChallengePrimeiraFase.Contatos.Domain
{
    public class ValidaPessoa : IValidaPessoa
    {
        public bool ValidaEmail(string? email)
        {
            if (!String.IsNullOrEmpty(email))
            {
                var emailVerificado = email.Trim();

                if (emailVerificado.EndsWith(".")) { return false; }

                try
                {
                    var enderecoEmail = new System.Net.Mail.MailAddress(email);
                    return enderecoEmail.Address == emailVerificado;
                }
                catch
                {
                    return false;
                }
            }
            else { return false; }
        }
    }
}
