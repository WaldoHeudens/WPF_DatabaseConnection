using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPF_DatabaseConnection.Migrations
{
    /// <inheritdoc />
    public partial class Gebruikers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gebruikers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gebruikersnaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wachtwoord = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruikers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prijzen_ProductId",
                table: "Prijzen",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prijzen_Producten_ProductId",
                table: "Prijzen",
                column: "ProductId",
                principalTable: "Producten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prijzen_Producten_ProductId",
                table: "Prijzen");

            migrationBuilder.DropTable(
                name: "Gebruikers");

            migrationBuilder.DropIndex(
                name: "IX_Prijzen_ProductId",
                table: "Prijzen");
        }
    }
}
