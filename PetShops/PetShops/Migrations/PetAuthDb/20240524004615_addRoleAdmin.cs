using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShops.Migrations.PetAuthDb
{
    /// <inheritdoc />
    public partial class addRoleAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    {"71e282d3- 76ca- 485e - b094- eff013337fa5","71e282d3- 76ca- 485e - b094- eff013337fa5", "Admin", "ADMIN" }
                  
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71e282d3-76ca-485e-b094-eff013337fa5");
        }
    }
}
