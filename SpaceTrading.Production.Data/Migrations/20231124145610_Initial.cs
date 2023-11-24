using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceTrading.Production.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourcesCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourcesCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitVolume = table.Column<float>(type: "real", nullable: false),
                    ResourceSizeId = table.Column<int>(type: "int", nullable: false),
                    ResourceCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_ResourceSizes_ResourceSizeId",
                        column: x => x.ResourceSizeId,
                        principalTable: "ResourceSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resources_ResourcesCategories_ResourceCategoryId",
                        column: x => x.ResourceCategoryId,
                        principalTable: "ResourcesCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceCategoryId",
                table: "Resources",
                column: "ResourceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceSizeId",
                table: "Resources",
                column: "ResourceSizeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "ResourceSizes");

            migrationBuilder.DropTable(
                name: "ResourcesCategories");
        }
    }
}
