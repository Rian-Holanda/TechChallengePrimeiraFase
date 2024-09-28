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

        int InserirPessoa(PessoasEntity pessoaEntity);
        bool AlterarPessoa(PessoasEntity pessoaEntity);
        bool ExcluirPessoa(int idPessoa);
        PessoasEntity? GetPessoa(int idPessoa);
        List<PessoasEntity>? GetPessoas();
    }
}
