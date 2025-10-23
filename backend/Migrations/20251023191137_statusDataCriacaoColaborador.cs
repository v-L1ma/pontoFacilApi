using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pontoFacilApi.Migrations
{
    /// <inheritdoc />
    public partial class statusDataCriacaoColaborador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Colaboradores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "dataCriacao",
                table: "Colaboradores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Colaboradores");

            migrationBuilder.DropColumn(
                name: "dataCriacao",
                table: "Colaboradores");
        }
    }
}
