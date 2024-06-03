using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GC_Subscription.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerZipCityRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Customer");

            migrationBuilder.AddColumn<int>(
                name: "Zip",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ZipCity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zip = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZipCity", x => x.Id);
                    table.UniqueConstraint("AK_ZipCity_Zip", x => x.Zip);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_ZipCity_Zip",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "ZipCity");

            migrationBuilder.DropIndex(
                name: "IX_Customer_Zip",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
