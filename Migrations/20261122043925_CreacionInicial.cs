using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LenteVivo.Migrations
{
    /// <inheritdoc />
    public partial class CreacionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paquetes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Duracion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FotosEditadas = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Video = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paquetes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    TipoSesion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaqueteId = table.Column<int>(type: "int", nullable: false),
                    Adicionales = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Invitados = table.Column<int>(type: "int", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Paquetes_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Paquetes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Paquetes",
                columns: new[] { "Id", "Duracion", "FotosEditadas", "Nombre", "Precio", "Video" },
                values: new object[,]
                {
                    { 1, "2 horas", "30", "Básico", 450000m, "No incluye" },
                    { 2, "4 horas", "60", "Estándar", 750000m, "No incluye" },
                    { 3, "Día completo", "120", "Premium", 1400000m, "Resumen de 3 min" },
                    { 4, "Día completo", "Todas las tomadas", "Premium Plus", 1900000m, "Resumen + tráiler" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Paquetes_Nombre",
                table: "Paquetes",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_PaqueteId",
                table: "Solicitudes",
                column: "PaqueteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Solicitudes");

            migrationBuilder.DropTable(
                name: "Paquetes");
        }
    }
}
