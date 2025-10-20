using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pontoFacilApi.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoCampoCPF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Colaboradores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Colaboradores");
        }
    }
}
