using Microsoft.EntityFrameworkCore.Migrations;

namespace TicaretApp.DataAccess.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Karts",
                columns: table => new
                {
                    KartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karts", x => x.KartId);
                });

            migrationBuilder.CreateTable(
                name: "Kategoris",
                columns: table => new
                {
                    KategoriId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoris", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Kitaps",
                columns: table => new
                {
                    KitapId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YazarId = table.Column<int>(nullable: false),
                    KitapturId = table.Column<int>(nullable: false),
                    YayineviId = table.Column<int>(nullable: false),
                    YorumId = table.Column<int>(nullable: false),
                    KitapAdi = table.Column<string>(nullable: true),
                    Yayinyili = table.Column<string>(nullable: true),
                    StokAdedi = table.Column<int>(nullable: true),
                    Fiyati = table.Column<decimal>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Aciklama = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitaps", x => x.KitapId);
                });

            migrationBuilder.CreateTable(
                name: "KartItem",
                columns: table => new
                {
                    KartItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KitapId = table.Column<int>(nullable: false),
                    KartId = table.Column<int>(nullable: false),
                    Adet = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KartItem", x => x.KartItemId);
                    table.ForeignKey(
                        name: "FK_KartItem_Karts_KartId",
                        column: x => x.KartId,
                        principalTable: "Karts",
                        principalColumn: "KartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KartItem_Kitaps_KitapId",
                        column: x => x.KitapId,
                        principalTable: "Kitaps",
                        principalColumn: "KitapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KitapKategori",
                columns: table => new
                {
                    KitapId = table.Column<int>(nullable: false),
                    KategoriId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitapKategori", x => new { x.KitapId, x.KategoriId });
                    table.ForeignKey(
                        name: "FK_KitapKategori_Kategoris_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategoris",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KitapKategori_Kitaps_KitapId",
                        column: x => x.KitapId,
                        principalTable: "Kitaps",
                        principalColumn: "KitapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KartItem_KartId",
                table: "KartItem",
                column: "KartId");

            migrationBuilder.CreateIndex(
                name: "IX_KartItem_KitapId",
                table: "KartItem",
                column: "KitapId");

            migrationBuilder.CreateIndex(
                name: "IX_KitapKategori_KategoriId",
                table: "KitapKategori",
                column: "KategoriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KartItem");

            migrationBuilder.DropTable(
                name: "KitapKategori");

            migrationBuilder.DropTable(
                name: "Karts");

            migrationBuilder.DropTable(
                name: "Kategoris");

            migrationBuilder.DropTable(
                name: "Kitaps");
        }
    }
}
