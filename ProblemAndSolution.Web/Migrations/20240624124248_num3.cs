using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProblemAndSolution.Web.Migrations
{
    public partial class num3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogsComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogsComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogsComment_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c5e174e-3b0e-446f-86af-483d56fd7210"),
                column: "ConcurrencyStamp",
                value: "638548513679824247");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e943ffbf-65a4-4d42-bb74-f2ca9ea8d22a"),
                column: "ConcurrencyStamp",
                value: "638548513679824313");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f3d96ce-76ec-4992-911a-33ceb81fa29d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10c82955-9286-4773-ad67-165f4bce199e", "AQAAAAEAACcQAAAAEBjrWA3NwishcmRLqPhNWf/XflnNVRMtWWvj3ss7HI//gs7Li0KA+JUOIfb75ZBfPA==", "818c4b44-4433-4bab-91e4-a3a99a21bd10" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e9b3be8c-99c5-42c7-8f2e-1eb39f6d9125"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8eb7b686-abd4-4f27-b6fc-50652ccdd778", "AQAAAAEAACcQAAAAEP0mpZCrwMdQ3UBSqCdlUCDdjpLtBW8VzWiU5OuTIon0/x3tUiRadT2OPYPZugyiuQ==", "790908c1-2085-46ef-a583-984fb715aef3" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogsComment_BlogId",
                table: "BlogsComment",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogsComment");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c5e174e-3b0e-446f-86af-483d56fd7210"),
                column: "ConcurrencyStamp",
                value: "638532731416139406");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e943ffbf-65a4-4d42-bb74-f2ca9ea8d22a"),
                column: "ConcurrencyStamp",
                value: "638532731416139425");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f3d96ce-76ec-4992-911a-33ceb81fa29d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1b87eb1-eb07-4f42-891b-72439dac90bf", "AQAAAAEAACcQAAAAEAFx1NpfgyELq74LXjjh1CR7FTDlDXCm9Q4L1FBPjTbMTx1shLcaMtnHaJsdWWzpQA==", "32004877-b3b9-49eb-9d61-f4bcd5f2376c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e9b3be8c-99c5-42c7-8f2e-1eb39f6d9125"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4df1abfa-4267-4871-88fa-23a98fa82b57", "AQAAAAEAACcQAAAAEE2ty+kDD7BnLtYz3x9DnQCEOrHHxUZbFvIjZKsW0q2FNPzRr2yog0HV1k1/qrhyhQ==", "35c139ed-e1cf-4033-a975-c62c5c74a614" });
        }
    }
}
