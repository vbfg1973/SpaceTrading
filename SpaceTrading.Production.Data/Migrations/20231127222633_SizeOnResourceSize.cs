using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceTrading.Production.Data.Migrations
{
    /// <inheritdoc />
    public partial class SizeOnResourceSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "ResourceSizes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "ResourceSizes");
        }
    }
}
