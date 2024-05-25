using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_TechChallengePrimeiraFase.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoContatoPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "tb_ContatoPessoa",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TipoContatoPessoa",
                table: "tb_ContatoPessoa",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoContatoPessoa",
                table: "tb_ContatoPessoa");

            migrationBuilder.AlterColumn<int>(
                name: "Numero",
                table: "tb_ContatoPessoa",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
