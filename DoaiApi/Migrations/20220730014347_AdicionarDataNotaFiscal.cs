using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoaiApi.Migrations
{
    public partial class AdicionarDataNotaFiscal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscal_Instituicoes_InstituicaoId",
                table: "NotaFiscal");

            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscal_Usuario_UsuarioId",
                table: "NotaFiscal");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "NotaFiscal",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstituicaoId",
                table: "NotaFiscal",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEnvio",
                table: "NotaFiscal",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_NotaFiscal_Instituicoes_InstituicaoId",
                table: "NotaFiscal",
                column: "InstituicaoId",
                principalTable: "Instituicoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotaFiscal_Usuario_UsuarioId",
                table: "NotaFiscal",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscal_Instituicoes_InstituicaoId",
                table: "NotaFiscal");

            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscal_Usuario_UsuarioId",
                table: "NotaFiscal");

            migrationBuilder.DropColumn(
                name: "DataEnvio",
                table: "NotaFiscal");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "NotaFiscal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "InstituicaoId",
                table: "NotaFiscal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
