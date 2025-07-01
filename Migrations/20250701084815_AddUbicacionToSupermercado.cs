using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComparadorPreciosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUbicacionToSupermercado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ubicacion",
                table: "Supermercados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "Supermercados");
        }
    }
}
