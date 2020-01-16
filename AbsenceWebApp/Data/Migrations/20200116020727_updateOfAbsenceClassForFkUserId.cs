using Microsoft.EntityFrameworkCore.Migrations;

namespace AbsenceWebApp.Data.Migrations
{
    public partial class updateOfAbsenceClassForFkUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absence_AspNetUsers_ApplicationUserId",
                table: "Absence");

            migrationBuilder.DropIndex(
                name: "IX_Absence_ApplicationUserId",
                table: "Absence");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Absence");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Absence",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Absence_UserID",
                table: "Absence",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Absence_AspNetUsers_UserID",
                table: "Absence",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absence_AspNetUsers_UserID",
                table: "Absence");

            migrationBuilder.DropIndex(
                name: "IX_Absence_UserID",
                table: "Absence");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Absence",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Absence",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Absence_ApplicationUserId",
                table: "Absence",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absence_AspNetUsers_ApplicationUserId",
                table: "Absence",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
