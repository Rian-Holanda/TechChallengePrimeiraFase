using Entities_TechChallengePrimeiraFase.Entities;
using Infrastructure_TechChallengePrimeiraFase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<PessoasEntity> Pessoas { get; set; }
    public DbSet<ContatosPessoaEntity> ContatosPessoas { get; set; }
    public DbSet<RegioesEntity> Regioes { get; set; }
    public DbSet<RegioesCodigosAreasEntity> RegioesCodigosAreas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("ConnectionString");

            if (!string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PessoasEntity>(entity =>
        {
            entity.ToTable("tb_Pessoa");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.Email).IsRequired();
        });

        modelBuilder.Entity<ContatosPessoaEntity>(entity =>
        {
            entity.ToTable("tb_ContatoPessoa");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Numero).IsRequired();
            entity.Property(e => e.TipoContatoPessoa).IsRequired();

            entity.HasOne(d => d.Pessoa)
                .WithMany(p => p.ContatoPessoas)
                .HasForeignKey(d => d.IdPessoa);

            entity.HasOne(d => d.Regiao)
                .WithMany()
                .HasForeignKey(d => d.IdRegiao);
        });


        modelBuilder.Entity<RegioesEntity>(entity =>
        {
            entity.ToTable("tb_Regiao");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Sigla).IsRequired();
        });

        modelBuilder.Entity<RegioesCodigosAreasEntity>(entity =>
        {
            entity.ToTable("tb_RegiaoCodigoArea");
            entity.HasKey(e => e.Id);


            entity.HasOne(d => d.Regiao)
                .WithMany(p => p.RegiaoCodigoAreas)
                .HasForeignKey(d => d.IdRegiao);
        });

        base.OnModelCreating(modelBuilder);
    }
}