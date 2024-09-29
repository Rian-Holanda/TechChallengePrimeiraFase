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

    public DbSet<PessoaEntity> Pessoas { get; set; }
    public DbSet<ContatoPessoaEntity> ContatosPessoas { get; set; }
    public DbSet<RegiaoEntity> Regioes { get; set; }
    public DbSet<RegiaoCodigoAreaEntity> RegioesCodigosAreas { get; set; }

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
        modelBuilder.Entity<PessoaEntity>(entity =>
        {
            entity.ToTable("tb_Pessoa");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.Email).IsRequired();
        });

        modelBuilder.Entity<ContatoPessoaEntity>(entity =>
        {
            entity.ToTable("tb_ContatoPessoa");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Numero).IsRequired();
            entity.Property(e => e.TipoContatoPessoa).IsRequired();

            entity.HasOne(d => d.Pessoa)
                .WithMany(p => p.ContatoPessoa)
                .HasForeignKey(d => d.IdPessoa);

            entity.HasOne(d => d.Regiao)
                .WithMany()
                .HasForeignKey(d => d.IdRegiao);
        });


        modelBuilder.Entity<RegiaoEntity>(entity =>
        {
            entity.ToTable("tb_Regiao");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Sigla).IsRequired();
        });

        modelBuilder.Entity<RegiaoCodigoAreaEntity>(entity =>
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