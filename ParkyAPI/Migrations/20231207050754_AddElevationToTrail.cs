using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkyAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddElevationToTrail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Elevation",
                table: "Trails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Elevation",
                table: "Trails");
        }
    }
}
