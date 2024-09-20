using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProblemAndSolution.Web.Migrations
{
    public partial class num2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Visible",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c5e174e-3b0e-446f-86af-483d56fd7210"),
                column: "ConcurrencyStamp",
                value: "638623502723262285");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e943ffbf-65a4-4d42-bb74-f2ca9ea8d22a"),
                column: "ConcurrencyStamp",
                value: "638623502723262312");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f3d96ce-76ec-4992-911a-33ceb81fa29d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5c2a6de-1883-4b28-9bd0-ed49d9d7e5f8", "AQAAAAEAACcQAAAAEBPPKj5i43/wH5oZ/UaJZ0fJq/TXiNRNX4y+y7pp0Amb8t55WDnrUtjrjOpJePS0eA==", "b1aabe5c-1e62-4ff6-9650-3c48aa2c477a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e9b3be8c-99c5-42c7-8f2e-1eb39f6d9125"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "968d1257-0b4d-48b5-b8e0-e2e6a0936d4d", "AQAAAAEAACcQAAAAEJqCSMv5sNVNGKRWBtk9aD4frSy7aqlizafV2fMf4FE1srHzpb+QAx+g2gBt3IrRlg==", "5424cb2f-bbe6-4593-8725-e764e04031d5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Visible",
                table: "Blogs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c5e174e-3b0e-446f-86af-483d56fd7210"),
                column: "ConcurrencyStamp",
                value: "638616951249412035");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e943ffbf-65a4-4d42-bb74-f2ca9ea8d22a"),
                column: "ConcurrencyStamp",
                value: "638616951249412059");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f3d96ce-76ec-4992-911a-33ceb81fa29d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e88dcc0a-7d8c-4ac6-9c25-d7e87870abfb", "AQAAAAEAACcQAAAAEAGoV3Iomp2BBOP7zGEyrt2m0M5HhXzbfZjIikE7JG0to99HiXWnx2H3yYv8WA7T0A==", "91996435-5803-4803-a1ed-e18b64d031c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e9b3be8c-99c5-42c7-8f2e-1eb39f6d9125"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e432d88-844f-45ed-a96e-c8ffba358a77", "AQAAAAEAACcQAAAAEMFj+jgCLtiNmg9iMEWzfMvULUBhSDAt+gxC3SbMsYFN5JcqPga0cCpmliw1eobmog==", "1b7dfd3a-7055-4439-a3e0-4fe605a1214a" });
        }
    }
}
