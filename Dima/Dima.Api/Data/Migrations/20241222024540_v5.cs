using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dima.Api.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
