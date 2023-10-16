using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPF_DatabaseConnection.Migrations
{
    /// <inheritdoc />
    public partial class ProductGecreerd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Gecreeerd",
                table: "Producten",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gecreeerd",
                table: "Producten");
        }
    }
}
