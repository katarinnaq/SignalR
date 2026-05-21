using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjekatSignalR.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChatTabele : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grupe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivatnePoruke",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosiljalacId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrimalacId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoslatoU = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivatnePoruke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivatnePoruke_AspNetUsers_PosiljalacId",
                        column: x => x.PosiljalacId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrivatnePoruke_AspNetUsers_PrimalacId",
                        column: x => x.PrimalacId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClanoviGrupe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrupaId = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanoviGrupe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClanoviGrupe_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClanoviGrupe_Grupe_GrupaId",
                        column: x => x.GrupaId,
                        principalTable: "Grupe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrupnePoruke",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrupaId = table.Column<int>(type: "int", nullable: false),
                    PosiljalacId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Poruka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumSlanja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupnePoruke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrupnePoruke_AspNetUsers_PosiljalacId",
                        column: x => x.PosiljalacId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupnePoruke_Grupe_GrupaId",
                        column: x => x.GrupaId,
                        principalTable: "Grupe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClanoviGrupe_GrupaId",
                table: "ClanoviGrupe",
                column: "GrupaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClanoviGrupe_KorisnikId",
                table: "ClanoviGrupe",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupnePoruke_GrupaId",
                table: "GrupnePoruke",
                column: "GrupaId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupnePoruke_PosiljalacId",
                table: "GrupnePoruke",
                column: "PosiljalacId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivatnePoruke_PosiljalacId",
                table: "PrivatnePoruke",
                column: "PosiljalacId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivatnePoruke_PrimalacId",
                table: "PrivatnePoruke",
                column: "PrimalacId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClanoviGrupe");

            migrationBuilder.DropTable(
                name: "GrupnePoruke");

            migrationBuilder.DropTable(
                name: "PrivatnePoruke");

            migrationBuilder.DropTable(
                name: "Grupe");
        }
    }
}
