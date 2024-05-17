using Entities_TechChallengePrimeiraFase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_TechChallengePrimeiraFase
{
    public interface IApplicationDbContext
    {
        DbSet<Pessoa> Pessoas { get; set; }
        DbSet<ContatoPessoa> ContatoPessoas { get; set; }
        DbSet<Regiao> Regioes { get; set; }
        DbSet<RegiaoCodigoArea> RegiaoCodigoAreas { get; set; }
    }
}
