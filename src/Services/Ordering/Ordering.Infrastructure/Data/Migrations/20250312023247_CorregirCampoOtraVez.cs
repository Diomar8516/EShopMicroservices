using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorregirCampoOtraVez : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastModified",
                table: "Products",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "lastModified",
                table: "Orders",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "lastModified",
                table: "OrderItems",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "lastModified",
                table: "Customers",
                newName: "LastModified");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Products",
                newName: "lastModified");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Orders",
                newName: "lastModified");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "OrderItems",
                newName: "lastModified");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Customers",
                newName: "lastModified");
        }
    }
}
