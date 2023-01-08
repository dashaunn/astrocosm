using Microsoft.EntityFrameworkCore.Migrations;

namespace Astrocosm.Migrations
{
    public partial class UpdateAppUsercs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ZodiacSigns_SunSignId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "SunSignId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ZodiacSigns_SunSignId",
                table: "AspNetUsers",
                column: "SunSignId",
                principalTable: "ZodiacSigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ZodiacSigns_SunSignId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "SunSignId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ZodiacSigns_SunSignId",
                table: "AspNetUsers",
                column: "SunSignId",
                principalTable: "ZodiacSigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
