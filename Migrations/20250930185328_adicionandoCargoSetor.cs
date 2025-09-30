using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace pontoFacilApi.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoCargoSetor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargos_Setores_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Setores",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Administrativo" },
                    { 2, "Financeiro" },
                    { 3, "Recursos Humanos" },
                    { 4, "Comercial" },
                    { 5, "Tecnologia da Informação" },
                    { 6, "Logística" },
                    { 7, "Jurídico" },
                    { 8, "Marketing" },
                    { 9, "Produção" },
                    { 10, "Atendimento ao Cliente" }
                });

            migrationBuilder.InsertData(
                table: "Cargos",
                columns: new[] { "Id", "Nome", "SetorId" },
                values: new object[,]
                {
                    { 1, "Estagiário", 1 },
                    { 2, "Assistente Administrativo", 1 },
                    { 3, "Analista Administrativo", 1 },
                    { 4, "Coordenador Administrativo", 1 },
                    { 5, "Assistente Financeiro", 2 },
                    { 6, "Analista Financeiro", 2 },
                    { 7, "Gerente Financeiro", 2 },
                    { 8, "Analista de RH", 3 },
                    { 9, "Coordenador de RH", 3 },
                    { 10, "Recrutador", 3 },
                    { 11, "Vendedor", 4 },
                    { 12, "Representante Comercial", 4 },
                    { 13, "Gerente Comercial", 4 },
                    { 14, "Desenvolvedor", 5 },
                    { 15, "Analista de Sistemas", 5 },
                    { 16, "Administrador de Redes", 5 },
                    { 17, "Coordenador de TI", 5 },
                    { 18, "Auxiliar de Logística", 6 },
                    { 19, "Supervisor de Logística", 6 },
                    { 20, "Advogado", 7 },
                    { 21, "Assistente Jurídico", 7 },
                    { 22, "Analista de Marketing", 8 },
                    { 23, "Designer Gráfico", 8 },
                    { 24, "Social Media", 8 },
                    { 25, "Operador de Máquina", 9 },
                    { 26, "Supervisor de Produção", 9 },
                    { 27, "Atendente", 10 },
                    { 28, "Supervisor de Atendimento", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_SetorId",
                table: "Cargos",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_CargoId",
                table: "Usuario",
                column: "CargoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Setores");
        }
    }
}
