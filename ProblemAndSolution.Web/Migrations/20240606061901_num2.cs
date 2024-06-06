using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProblemAndSolution.Web.Migrations
{
    public partial class num2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Heading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Questions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c5e174e-3b0e-446f-86af-483d56fd7210"),
                column: "ConcurrencyStamp",
                value: "638252093743764147");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e943ffbf-65a4-4d42-bb74-f2ca9ea8d22a"),
                column: "ConcurrencyStamp",
                value: "638252093743764187");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f3d96ce-76ec-4992-911a-33ceb81fa29d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0748f07-7afd-4931-ae5f-a3dd39ebfd76", "AQAAAAEAACcQAAAAECGxojQuLAXrVlrWhj1ZszdCx1ITHxreY0MngBdg03+7r0vKZKfWz+sZ8Fg0L8fF1Q==", "0b18027a-6358-4837-9b6c-ca15375b38cd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e9b3be8c-99c5-42c7-8f2e-1eb39f6d9125"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e50b6dc6-dd11-4717-a15e-153f22794ae8", "AQAAAAEAACcQAAAAEGgQvhpI67IGrpWdzNGp4doKknPRkrq3pabd0GK6n7aS+HXnpIRBDTBfnytGo4EDTQ==", "fb4769da-9ce8-4c9e-9296-01c553cd8166" });
        }
    }
}
