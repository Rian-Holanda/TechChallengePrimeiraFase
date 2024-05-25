using Entities_TechChallengePrimeiraFase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_TechChallengePrimeiraFase
{
    public interface IApplicationDbContext
    {
        DbSet<PessoasEntity> Pessoas { get; set; }
        DbSet<ContatosPessoaEntity> ContatosPessoas { get; set; }
        DbSet<RegioesEntity> Regioes { get; set; }
        DbSet<RegioesCodigosAreasEntity> RegioesCodigosAreas { get; set; }
        int SaveChanges();
    }
}
