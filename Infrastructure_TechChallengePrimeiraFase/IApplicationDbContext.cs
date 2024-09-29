using Entities_TechChallengePrimeiraFase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_TechChallengePrimeiraFase
{
    public interface IApplicationDbContext
    {
        DbSet<PessoaEntity> Pessoas { get; set; }
        DbSet<ContatoPessoaEntity> ContatosPessoas { get; set; }
        DbSet<RegiaoEntity> Regioes { get; set; }
        DbSet<RegiaoCodigoAreaEntity> RegioesCodigosAreas { get; set; }
        int SaveChanges();
    }
}
