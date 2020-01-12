using Microsoft.EntityFrameworkCore.Migrations;

namespace KoleksiyoncuCom.DataAccess.Migrations
{
    public partial class updateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Sellers");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Sellers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Sellers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VerifiedSeller",
                table: "Sellers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastLoginDate",
                table: "Buyers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationDate",
                table: "Buyers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "VerifiedSeller",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Buyers");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
