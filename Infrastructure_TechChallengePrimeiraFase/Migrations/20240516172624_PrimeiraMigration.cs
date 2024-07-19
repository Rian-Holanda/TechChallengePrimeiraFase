using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_TechChallengePrimeiraFase.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Regiao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Regiao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_ContatoPessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    IdPessoa = table.Column<int>(type: "int", nullable: false),
                    IdRegiao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ContatoPessoa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_ContatoPessoa_tb_Pessoa_IdPessoa",
                        column: x => x.IdPessoa,
                        principalTable: "tb_Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_ContatoPessoa_tb_Regiao_IdRegiao",
                        column: x => x.IdRegiao,
                        principalTable: "tb_Regiao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_RegiaoCodigoArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DDD = table.Column<int>(type: "int", nullable: false),
                    IdRegiao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_RegiaoCodigoArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_RegiaoCodigoArea_tb_Regiao_IdRegiao",
                        column: x => x.IdRegiao,
                        principalTable: "tb_Regiao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_ContatoPessoa_IdPessoa",
                table: "tb_ContatoPessoa",
                column: "IdPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_tb_ContatoPessoa_IdRegiao",
                table: "tb_ContatoPessoa",
                column: "IdRegiao");

            migrationBuilder.CreateIndex(
                name: "IX_tb_RegiaoCodigoArea_IdRegiao",
                table: "tb_RegiaoCodigoArea",
                column: "IdRegiao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_ContatoPessoa");

            migrationBuilder.DropTable(
                name: "tb_RegiaoCodigoArea");

            migrationBuilder.DropTable(
                name: "tb_Pessoa");

            migrationBuilder.DropTable(
                name: "tb_Regiao");
        }
    }
}
