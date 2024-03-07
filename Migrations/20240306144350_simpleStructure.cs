using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taskApi.Migrations
{
    /// <inheritdoc />
    public partial class simpleStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_SalesMasters_InvoiceId",
                table: "SalesDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalesDetails_InvoiceId",
                table: "SalesDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
