using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroVentas.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistrosOperaciones",
                columns: table => new
                {
                    Pk = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Operacion = table.Column<string>(type: "TEXT", nullable: false),
                    Importe = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    Cliente = table.Column<string>(type: "TEXT", nullable: false),
                    Referencia = table.Column<string>(type: "TEXT", nullable: false),
                    Estatus = table.Column<string>(type: "TEXT", nullable: false),
                    Secreto = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosOperaciones", x => x.Pk);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosOperaciones_Referencia",
                table: "RegistrosOperaciones",
                column: "Referencia",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosOperaciones");
        }
    }
}
