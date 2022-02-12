using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcAgenda.Migrations
{
    public partial class CambioTareas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Departamentos_DepartamentoId",
                table: "Tareas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Tareas_TareaId",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_DepartamentoId",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_TareaId",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "TareaId",
                table: "Tareas");

            migrationBuilder.RenameColumn(
                name: "FechaNacimiento",
                table: "Tareas",
                newName: "HoraInicio");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Tareas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "Tareas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicio",
                table: "Tareas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraFin",
                table: "Tareas",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "FechaInicio",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "HoraFin",
                table: "Tareas");

            migrationBuilder.RenameColumn(
                name: "HoraInicio",
                table: "Tareas",
                newName: "FechaNacimiento");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "Tareas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Tareas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TareaId",
                table: "Tareas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_DepartamentoId",
                table: "Tareas",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_TareaId",
                table: "Tareas",
                column: "TareaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Departamentos_DepartamentoId",
                table: "Tareas",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Tareas_TareaId",
                table: "Tareas",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
