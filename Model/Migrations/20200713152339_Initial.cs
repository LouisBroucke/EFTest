using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Landen",
                columns: table => new
                {
                    ISOLandCode = table.Column<string>(maxLength: 2, nullable: false),
                    NISLandCode = table.Column<string>(maxLength: 3, nullable: true),
                    Naam = table.Column<string>(maxLength: 50, nullable: false),
                    AantalInwoners = table.Column<int>(nullable: false),
                    Oppervlakte = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Landen", x => x.ISOLandCode);
                });

            migrationBuilder.CreateTable(
                name: "Talen",
                columns: table => new
                {
                    ISOTaalCode = table.Column<string>(maxLength: 2, nullable: false),
                    NaamNL = table.Column<string>(maxLength: 50, nullable: false),
                    NaamTaal = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talen", x => x.ISOTaalCode);
                });

            migrationBuilder.CreateTable(
                name: "Steden",
                columns: table => new
                {
                    StadId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(maxLength: 50, nullable: false),
                    ISOLandCode = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steden", x => x.StadId);
                    table.ForeignKey(
                        name: "FK_Steden_Landen_ISOLandCode",
                        column: x => x.ISOLandCode,
                        principalTable: "Landen",
                        principalColumn: "ISOLandCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LandsTalen",
                columns: table => new
                {
                    ISOLandCode = table.Column<string>(maxLength: 2, nullable: false),
                    ISOTaalCode = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandsTalen", x => new { x.ISOLandCode, x.ISOTaalCode });
                    table.ForeignKey(
                        name: "FK_LandsTalen_Landen_ISOLandCode",
                        column: x => x.ISOLandCode,
                        principalTable: "Landen",
                        principalColumn: "ISOLandCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LandsTalen_Talen_ISOTaalCode",
                        column: x => x.ISOTaalCode,
                        principalTable: "Talen",
                        principalColumn: "ISOTaalCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Landen_NISLandCode",
                table: "Landen",
                column: "NISLandCode",
                unique: true,
                filter: "[NISLandCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LandsTalen_ISOTaalCode",
                table: "LandsTalen",
                column: "ISOTaalCode");

            migrationBuilder.CreateIndex(
                name: "IX_Steden_ISOLandCode",
                table: "Steden",
                column: "ISOLandCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LandsTalen");

            migrationBuilder.DropTable(
                name: "Steden");

            migrationBuilder.DropTable(
                name: "Talen");

            migrationBuilder.DropTable(
                name: "Landen");
        }
    }
}
