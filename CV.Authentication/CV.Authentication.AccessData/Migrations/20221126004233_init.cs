using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CV.Authentication.AccessData.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccountState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 2, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VerificationCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResetCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime", nullable: true),
                    State = table.Column<int>(type: "int", maxLength: 2, nullable: false, defaultValue: 4)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_UserAccountState_State",
                        column: x => x.State,
                        principalTable: "UserAccountState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserAccountState",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Inactive" },
                    { 3, "Blocked" },
                    { 4, "Customer registration in progress" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_State",
                table: "User",
                column: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserAccountState");
        }
    }
}
