using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class MeasureAndMeasureTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measures_MeasureType_MeasureTypeId",
                table: "Measures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasureType",
                table: "MeasureType");

            migrationBuilder.RenameTable(
                name: "MeasureType",
                newName: "MeasureTypes");

            migrationBuilder.AddColumn<string>(
                name: "HomeNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasureTypes",
                table: "MeasureTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measures_MeasureTypes_MeasureTypeId",
                table: "Measures",
                column: "MeasureTypeId",
                principalTable: "MeasureTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measures_MeasureTypes_MeasureTypeId",
                table: "Measures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasureTypes",
                table: "MeasureTypes");

            migrationBuilder.DropColumn(
                name: "HomeNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "WorkNumber",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "MeasureTypes",
                newName: "MeasureType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasureType",
                table: "MeasureType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measures_MeasureType_MeasureTypeId",
                table: "Measures",
                column: "MeasureTypeId",
                principalTable: "MeasureType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
