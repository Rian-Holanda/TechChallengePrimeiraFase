using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_TechChallengePrimeiraFase.Entities;

namespace DataAccess_TechChallengePrimeiraFase.Regioes.Interface
{
    public interface IContatosPessoasCommand
    {

        int InserirContatoPessoa(ContatosPessoaEntity contatoPessoaEntity);
        bool AlterarContatoPessoa(ContatosPessoaEntity contatoPessoaEntity, int idContatoPessoa);
        bool ExcluirContatoPessoa(int idContatoPessoa);
        ContatosPessoaEntity? GetContatoPessoa(int idContatoPessoa);
        List<ContatosPessoaEntity>? GetContatosPessoas();
    }
}
