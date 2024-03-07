using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taskApi.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_SalesMasters_InvoiceIdMaster",
                table: "SalesDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalesDetails_InvoiceIdMaster",
                table: "SalesDetails");

            migrationBuilder.DropColumn(
                name: "InvoiceIdMaster",
                table: "SalesDetails");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "SalesDetails",
                newName: "InvoiceId");

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "SalesDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetails_InvoiceId",
                table: "SalesDetails",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_SalesMasters_InvoiceId",
                table: "SalesDetails",
                column: "InvoiceId",
                principalTable: "SalesMasters",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_SalesMasters_InvoiceId",
                table: "SalesDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalesDetails_InvoiceId",
                table: "SalesDetails");

            migrationBuilder.DropColumn(
                name: "Item",
                table: "SalesDetails");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "SalesDetails",
                newName: "ItemId");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceIdMaster",
                table: "SalesDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetails_InvoiceIdMaster",
                table: "SalesDetails",
                column: "InvoiceIdMaster");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_SalesMasters_InvoiceIdMaster",
                table: "SalesDetails",
                column: "InvoiceIdMaster",
                principalTable: "SalesMasters",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
