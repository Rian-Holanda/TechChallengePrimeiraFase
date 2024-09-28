using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_TechChallengePrimeiraFase.Entities;

namespace DataAccess_TechChallengePrimeiraFase.Contatos.Interface
{
    public interface IContatosPessoasCommand
    {

        Task<int> InserirContatoPessoa(ContatosPessoaEntity contatoPessoaEntity);
        Task<bool> AlterarContatoPessoa(ContatosPessoaEntity contatoPessoaEntity, int idContatoPessoa);
        Task<bool> ExcluirContatoPessoa(int idContatoPessoa);
        ContatosPessoaEntity? GetContatoPessoa(int idContatoPessoa);
        List<ContatosPessoaEntity>? GetContatosPessoas();
    }
}
