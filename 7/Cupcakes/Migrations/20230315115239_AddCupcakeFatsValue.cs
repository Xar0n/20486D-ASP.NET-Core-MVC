using Microsoft.EntityFrameworkCore.Migrations;

namespace Cupcakes.Migrations
{
    public partial class AddCupcakeFatsValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FatsValue",
                table: "Cupcakes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Cupcakes",
                keyColumn: "CupcakeId",
                keyValue: 1,
                column: "FatsValue",
                value: 56);

            migrationBuilder.UpdateData(
                table: "Cupcakes",
                keyColumn: "CupcakeId",
                keyValue: 2,
                column: "FatsValue",
                value: 67);

            migrationBuilder.UpdateData(
                table: "Cupcakes",
                keyColumn: "CupcakeId",
                keyValue: 3,
                column: "FatsValue",
                value: 87);

            migrationBuilder.UpdateData(
                table: "Cupcakes",
                keyColumn: "CupcakeId",
                keyValue: 4,
                column: "FatsValue",
                value: 46);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FatsValue",
                table: "Cupcakes");
        }
    }
}
