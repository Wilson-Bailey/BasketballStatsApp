using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballStatsApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Players");
        }
    }
}
