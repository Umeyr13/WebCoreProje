using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCoreProje.Migrations
{
    public partial class Addcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilImagefileName",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "No_Image_Available.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilImagefileName",
                table: "User");
        }
    }
}
