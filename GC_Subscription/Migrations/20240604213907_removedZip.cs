using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GC_Subscription.Migrations
{
    /// <inheritdoc />
    public partial class removedZip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_ZipCity_Zip",
                table: "Customer");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ZipCity_Zip",
                table: "ZipCity");

            migrationBuilder.DropIndex(
                name: "IX_Customer_Zip",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ZipCity_Zip",
                table: "ZipCity",
                column: "Zip");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Zip",
                table: "Customer",
                column: "Zip",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_ZipCity_Zip",
                table: "Customer",
                column: "Zip",
                principalTable: "ZipCity",
                principalColumn: "Zip",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
