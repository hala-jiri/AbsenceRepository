using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbsenceWebApp.Data.Migrations
{
    public partial class addAbsenceToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Absence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    DatetimeFrom = table.Column<DateTime>(nullable: false),
                    DatetimeTo = table.Column<DateTime>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    DatetimeOfCreated = table.Column<DateTime>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    ApprovedDate = table.Column<DateTime>(nullable: false),
                    ApprovedByUserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absence", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absence");
        }
    }
}
