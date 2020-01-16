using Microsoft.EntityFrameworkCore.Migrations;

namespace AbsenceWebApp.Data.Migrations
{
    public partial class updateAbsenceClassFkForApprovedByAndNotRequireUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absence_AspNetUsers_UserID",
                table: "Absence");

            migrationBuilder.DropColumn(
                name: "ApprovedByUserName",
                table: "Absence");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Absence",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedByUserID",
                table: "Absence",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Absence_ApprovedByUserID",
                table: "Absence",
                column: "ApprovedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Absence_AspNetUsers_ApprovedByUserID",
                table: "Absence",
                column: "ApprovedByUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Absence_AspNetUsers_UserID",
                table: "Absence",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absence_AspNetUsers_ApprovedByUserID",
                table: "Absence");

            migrationBuilder.DropForeignKey(
                name: "FK_Absence_AspNetUsers_UserID",
                table: "Absence");

            migrationBuilder.DropIndex(
                name: "IX_Absence_ApprovedByUserID",
                table: "Absence");

            migrationBuilder.DropColumn(
                name: "ApprovedByUserID",
                table: "Absence");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Absence",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedByUserName",
                table: "Absence",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Absence_AspNetUsers_UserID",
                table: "Absence",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
