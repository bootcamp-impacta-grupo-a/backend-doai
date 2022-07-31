using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoaiApi.Migrations
{
    public partial class RelacaoUserInstituicao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "InstituicaoId",
                table: "NotaFiscal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "NotaFiscal",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_InstituicaoId",
                table: "NotaFiscal",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_UsuarioId",
                table: "NotaFiscal",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotaFiscal_Instituicoes_InstituicaoId",
                table: "NotaFiscal",
                column: "InstituicaoId",
                principalTable: "Instituicoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotaFiscal_Usuario_UsuarioId",
                table: "NotaFiscal",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscal_Instituicoes_InstituicaoId",
                table: "NotaFiscal");

            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscal_Usuario_UsuarioId",
                table: "NotaFiscal");

            migrationBuilder.DropIndex(
                name: "IX_NotaFiscal_InstituicaoId",
                table: "NotaFiscal");

            migrationBuilder.DropIndex(
                name: "IX_NotaFiscal_UsuarioId",
                table: "NotaFiscal");

            migrationBuilder.DropColumn(
                name: "InstituicaoId",
                table: "NotaFiscal");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "NotaFiscal");

        }
    }
}
