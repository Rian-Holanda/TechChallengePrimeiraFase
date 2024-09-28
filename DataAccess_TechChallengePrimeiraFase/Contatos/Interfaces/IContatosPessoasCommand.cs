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

        Task<int> InserirContatoPessoa(ContatoPessoaEntity contatoPessoaEntity);
        Task<bool> AlterarContatoPessoa(ContatoPessoaEntity contatoPessoaEntity, int idContatoPessoa);
        Task<bool> ExcluirContatoPessoa(int idContatoPessoa);
        ContatoPessoaEntity? GetContatoPessoa(int idContatoPessoa);
        List<ContatoPessoaEntity>? GetContatosPessoas();
    }
}
