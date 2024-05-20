﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure_TechChallengePrimeiraFase.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240520034933_AlteracaoContatoPessoa")]
    partial class AlteracaoContatoPessoa
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities_TechChallengePrimeiraFase.Entities.ContatosPessoaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.Property<int>("IdRegiao")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoContatoPessoa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdPessoa");

                    b.HasIndex("IdRegiao");

                    b.ToTable("tb_ContatoPessoa", (string)null);
                });

            modelBuilder.Entity("Entities_TechChallengePrimeiraFase.Entities.PessoasEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tb_Pessoa", (string)null);
                });

            modelBuilder.Entity("Entities_TechChallengePrimeiraFase.Entities.RegioesCodigosAreasEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DDD")
                        .HasColumnType("int");

                    b.Property<int>("IdRegiao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdRegiao");

                    b.ToTable("tb_RegiaoCodigoArea", (string)null);
                });

            modelBuilder.Entity("Entities_TechChallengePrimeiraFase.Entities.RegioesEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tb_Regiao", (string)null);
                });

            modelBuilder.Entity("Entities_TechChallengePrimeiraFase.Entities.ContatosPessoaEntity", b =>
                {
                    b.HasOne("Entities_TechChallengePrimeiraFase.Entities.PessoasEntity", "Pessoa")
                        .WithMany("ContatoPessoas")
                        .HasForeignKey("IdPessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities_TechChallengePrimeiraFase.Entities.RegioesEntity", "Regiao")
                        .WithMany()
                        .HasForeignKey("IdRegiao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");

                    b.Navigation("Regiao");
                });

            modelBuilder.Entity("Entities_TechChallengePrimeiraFase.Entities.RegioesCodigosAreasEntity", b =>
                {
                    b.HasOne("Entities_TechChallengePrimeiraFase.Entities.RegioesEntity", "Regiao")
                        .WithMany("RegiaoCodigoAreas")
                        .HasForeignKey("IdRegiao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Regiao");
                });

            modelBuilder.Entity("Entities_TechChallengePrimeiraFase.Entities.PessoasEntity", b =>
                {
                    b.Navigation("ContatoPessoas");
                });

            modelBuilder.Entity("Entities_TechChallengePrimeiraFase.Entities.RegioesEntity", b =>
                {
                    b.Navigation("RegiaoCodigoAreas");
                });
#pragma warning restore 612, 618
        }
    }
}
