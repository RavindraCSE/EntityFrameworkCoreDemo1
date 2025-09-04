using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp_EfCore1.Migrations
{
    /// <inheritdoc />
    public partial class AddCityTabletoDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Department",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_CityId",
                table: "Department",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Cities_CityId",
                table: "Department",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Cities_CityId",
                table: "Department");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Department_CityId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Department");
        }
    }
}
