using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_TechChallengePrimeiraFase.Entities;

namespace DataAccess_TechChallengePrimeiraFase.Contatos.Interface
{
    public interface IPessoasCommand
    {

        int InserirPessoa(PessoaEntity pessoaEntity);
        bool AlterarPessoa(PessoaEntity pessoaEntity);
        bool ExcluirPessoa(int idPessoa);
        PessoaEntity? GetPessoa(int idPessoa);
        List<PessoaEntity>? GetPessoas();
    }
}
