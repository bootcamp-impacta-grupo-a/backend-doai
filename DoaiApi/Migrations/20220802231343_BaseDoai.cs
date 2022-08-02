using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace DoaiApi.Migrations
{
    public partial class BaseDoai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instituicoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    RazaoSocial = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    Estado = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituicoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Senha = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Login = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotaFiscal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    ContentType = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    ArrayBytes = table.Column<byte[]>(type: "longblob", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "datetime", nullable: false),
                    InstituicaoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaFiscal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotaFiscal_Instituicoes_InstituicaoId",
                        column: x => x.InstituicaoId,
                        principalTable: "Instituicoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotaFiscal_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_InstituicaoId",
                table: "NotaFiscal",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_UsuarioId",
                table: "NotaFiscal",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotaFiscal");

            migrationBuilder.DropTable(
                name: "Instituicoes");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
