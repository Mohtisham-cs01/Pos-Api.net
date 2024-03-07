using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taskApi.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_Items_ItemId",
                table: "SalesDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalesDetails_ItemId",
                table: "SalesDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SalesDetails_ItemId",
                table: "SalesDetails",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_Items_ItemId",
                table: "SalesDetails",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
