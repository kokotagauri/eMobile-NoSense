using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileList.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mobiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Weight = table.Column<decimal>(nullable: false),
                    Resolution = table.Column<string>(nullable: true),
                    Processor = table.Column<string>(nullable: true),
                    Memory = table.Column<string>(nullable: true),
                    OperatingSystem = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ManufacturerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mobiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mobiles_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MobileImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Src = table.Column<string>(nullable: true),
                    MobileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileImages_Mobiles_MobileId",
                        column: x => x.MobileId,
                        principalTable: "Mobiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MobileThumbnail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Src = table.Column<string>(nullable: true),
                    MobileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileThumbnail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileThumbnail_Mobiles_MobileId",
                        column: x => x.MobileId,
                        principalTable: "Mobiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MobileVideos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Src = table.Column<string>(nullable: true),
                    ThumbnailSrc = table.Column<string>(nullable: true),
                    MobileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileVideos_Mobiles_MobileId",
                        column: x => x.MobileId,
                        principalTable: "Mobiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobileImages_MobileId",
                table: "MobileImages",
                column: "MobileId");

            migrationBuilder.CreateIndex(
                name: "IX_Mobiles_ManufacturerId",
                table: "Mobiles",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileThumbnail_MobileId",
                table: "MobileThumbnail",
                column: "MobileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MobileVideos_MobileId",
                table: "MobileVideos",
                column: "MobileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileImages");

            migrationBuilder.DropTable(
                name: "MobileThumbnail");

            migrationBuilder.DropTable(
                name: "MobileVideos");

            migrationBuilder.DropTable(
                name: "Mobiles");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
