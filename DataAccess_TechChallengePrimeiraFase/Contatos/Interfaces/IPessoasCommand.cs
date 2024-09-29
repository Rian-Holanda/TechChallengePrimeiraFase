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

        Task<int> InserirPessoa(PessoaEntity pessoaEntity);
        Task<bool> AlterarPessoa(PessoaEntity pessoaEntity, int id);
        Task<bool> ExcluirPessoa(int idPessoa);
        PessoaEntity? GetPessoa(int idPessoa);
        List<PessoaEntity>? GetPessoas();
    }
}
