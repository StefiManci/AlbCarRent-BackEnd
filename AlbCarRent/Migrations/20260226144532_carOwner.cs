using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlbCarRent.Migrations
{
    /// <inheritdoc />
    public partial class carOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarOwner",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarOwner",
                table: "Bookings");
        }
    }
}
