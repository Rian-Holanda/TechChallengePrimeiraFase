using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_TechChallengePrimeiraFase.Entities;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Interface
{
    internal interface IPessoasCommand
    {

        int InserirPessoa(PessoasEntity pessoaEntity);
        bool AlterarPessoa(PessoasEntity pessoaEntity, int idPessoa);
        bool ExcluirPessoa(int idPessoa);
        PessoasEntity? GetPessoa(int idPessoa);
        List<PessoasEntity>? GetPessoas();
    }
}
