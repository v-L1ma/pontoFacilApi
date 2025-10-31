using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pontoFacilApi.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoDataExclusao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dataExclusao",
                table: "Colaboradores",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dataExclusao",
                table: "Colaboradores");
        }
    }
}
