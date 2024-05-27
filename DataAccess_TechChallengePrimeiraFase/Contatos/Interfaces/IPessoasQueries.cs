using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengePrimeiraFase.Contatos.Interfaces
{
    public interface IPessoasQueries
    {
        string? GetEmailPessoa(int idPessoa);
        string? GetNomePessoa(int idPessoa);
    }
}
