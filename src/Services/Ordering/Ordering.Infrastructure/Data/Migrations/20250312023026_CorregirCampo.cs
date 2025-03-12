using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorregirCampo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LasModifiedBy",
                table: "Products",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LasModifiedBy",
                table: "Orders",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LasModifiedBy",
                table: "OrderItems",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LasModifiedBy",
                table: "Customers",
                newName: "LastModifiedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Products",
                newName: "LasModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Orders",
                newName: "LasModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "OrderItems",
                newName: "LasModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Customers",
                newName: "LasModifiedBy");
        }
    }
}
