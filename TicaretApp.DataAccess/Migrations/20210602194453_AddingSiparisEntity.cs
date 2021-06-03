using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicaretApp.DataAccess.Migrations
{
    public partial class AddingSiparisEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Siparis",
                columns: table => new
                {
                    SiparisId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisNumber = table.Column<string>(nullable: true),
                    dateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    SiparisDurum = table.Column<int>(nullable: false),
                    OdemeTipi = table.Column<int>(nullable: false),
                    KullaniciAd = table.Column<string>(nullable: true),
                    KullaniciSoyad = table.Column<string>(nullable: true),
                    Adres = table.Column<string>(nullable: true),
                    Sehir = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    SiparisNot = table.Column<string>(nullable: true),
                    OdemeId = table.Column<string>(nullable: true),
                    OdemeToken = table.Column<string>(nullable: true),
                    ConversationId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparis", x => x.SiparisId);
                });

            migrationBuilder.CreateTable(
                name: "SiparisItem",
                columns: table => new
                {
                    SiparisItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisId = table.Column<int>(nullable: false),
                    KitapId = table.Column<int>(nullable: false),
                    Fiyat = table.Column<decimal>(nullable: false),
                    Adet = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiparisItem", x => x.SiparisItemId);
                    table.ForeignKey(
                        name: "FK_SiparisItem_Kitaps_KitapId",
                        column: x => x.KitapId,
                        principalTable: "Kitaps",
                        principalColumn: "KitapId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiparisItem_Siparis_SiparisId",
                        column: x => x.SiparisId,
                        principalTable: "Siparis",
                        principalColumn: "SiparisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiparisItem_KitapId",
                table: "SiparisItem",
                column: "KitapId");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisItem_SiparisId",
                table: "SiparisItem",
                column: "SiparisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiparisItem");

            migrationBuilder.DropTable(
                name: "Siparis");
        }
    }
}
