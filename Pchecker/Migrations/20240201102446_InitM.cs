using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektManager.Migrations
{
    /// <inheritdoc />
    public partial class InitM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Projekte",
                columns: table => new
                {
                    ProjektNr = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Auftraggeber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Stand = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Startpunkt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProjektLeiter = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeadLine = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekte", x => x.ProjektNr);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Abteilungen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AbBezeichung = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AbLeiter = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProjektNr = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abteilungen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abteilungen_Projekte_ProjektNr",
                        column: x => x.ProjektNr,
                        principalTable: "Projekte",
                        principalColumn: "ProjektNr");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Probleme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Bezug = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuftritsDatum = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    KW = table.Column<int>(type: "int", nullable: false),
                    Abteilung = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Initiator = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Kategorie = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Thema = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Maßnahme = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bewertung = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Termin = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ReTermin = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ProzessStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProjektNr = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Probleme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Probleme_Projekte_ProjektNr",
                        column: x => x.ProjektNr,
                        principalTable: "Projekte",
                        principalColumn: "ProjektNr",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Mitarbeiter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NachName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProjektStunden = table.Column<int>(type: "int", nullable: false),
                    AbteilungId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mitarbeiter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mitarbeiter_Abteilungen_AbteilungId",
                        column: x => x.AbteilungId,
                        principalTable: "Abteilungen",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Abteilungen_ProjektNr",
                table: "Abteilungen",
                column: "ProjektNr");

            migrationBuilder.CreateIndex(
                name: "IX_Mitarbeiter_AbteilungId",
                table: "Mitarbeiter",
                column: "AbteilungId");

            migrationBuilder.CreateIndex(
                name: "IX_Probleme_ProjektNr",
                table: "Probleme",
                column: "ProjektNr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mitarbeiter");

            migrationBuilder.DropTable(
                name: "Probleme");

            migrationBuilder.DropTable(
                name: "Abteilungen");

            migrationBuilder.DropTable(
                name: "Projekte");
        }
    }
}
