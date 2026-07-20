using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Henches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Henches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Formulas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SourceHench1Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceHench2Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetHenchId = table.Column<Guid>(type: "uuid", nullable: false),
                    SuccessRate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formulas_Henches_SourceHench1Id",
                        column: x => x.SourceHench1Id,
                        principalTable: "Henches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Formulas_Henches_SourceHench2Id",
                        column: x => x.SourceHench2Id,
                        principalTable: "Henches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Formulas_Henches_TargetHenchId",
                        column: x => x.TargetHenchId,
                        principalTable: "Henches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MapHenches",
                columns: table => new
                {
                    MapsId = table.Column<Guid>(type: "uuid", nullable: false),
                    HenchesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapHenches", x => new { x.MapsId, x.HenchesId });
                    table.ForeignKey(
                        name: "FK_MapHenches_Henches_HenchesId",
                        column: x => x.HenchesId,
                        principalTable: "Henches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapHenches_Maps_MapsId",
                        column: x => x.MapsId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Formulas_SourceHench1Id",
                table: "Formulas",
                column: "SourceHench1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Formulas_SourceHench2Id",
                table: "Formulas",
                column: "SourceHench2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Formulas_TargetHenchId",
                table: "Formulas",
                column: "TargetHenchId");

            migrationBuilder.CreateIndex(
                name: "IX_MapHenches_HenchesId",
                table: "MapHenches",
                column: "HenchesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Formulas");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "MapHenches");

            migrationBuilder.DropTable(
                name: "Henches");

            migrationBuilder.DropTable(
                name: "Maps");
        }
    }
}
