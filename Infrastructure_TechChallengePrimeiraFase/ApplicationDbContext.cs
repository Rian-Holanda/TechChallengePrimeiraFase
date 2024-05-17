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

    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<ContatoPessoa> ContatoPessoas { get; set; }
    public DbSet<Regiao> Regioes { get; set; }
    public DbSet<RegiaoCodigoArea> RegiaoCodigoAreas { get; set; }

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
        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.ToTable("tb_Pessoa");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.Email).IsRequired();
        });

        modelBuilder.Entity<ContatoPessoa>(entity =>
        {
            entity.ToTable("tb_ContatoPessoa");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Numero).IsRequired();

            entity.HasOne(d => d.Pessoa)
                .WithMany(p => p.ContatoPessoas)
                .HasForeignKey(d => d.IdPessoa);

            entity.HasOne(d => d.Regiao)
                .WithMany()
                .HasForeignKey(d => d.IdRegiao);
        });


        modelBuilder.Entity<Regiao>(entity =>
        {
            entity.ToTable("tb_Regiao");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Sigla).IsRequired();
        });

        modelBuilder.Entity<RegiaoCodigoArea>(entity =>
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