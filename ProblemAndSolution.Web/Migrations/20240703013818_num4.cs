using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProblemAndSolution.Web.Migrations
{
    public partial class num4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c5e174e-3b0e-446f-86af-483d56fd7210"),
                column: "ConcurrencyStamp",
                value: "638555890979761355");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e943ffbf-65a4-4d42-bb74-f2ca9ea8d22a"),
                column: "ConcurrencyStamp",
                value: "638555890979761385");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f3d96ce-76ec-4992-911a-33ceb81fa29d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51ce6039-8de8-4ee6-b08f-d581d9aa7173", "AQAAAAEAACcQAAAAEPsRiawA1j/v3NtgdAEy+I/26xb44XNmmmBH5zK88aDzQv6m2XvV6vXIXH0TUOVhbA==", "7d042802-2c05-43ba-b3d8-05fd70a6a14f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e9b3be8c-99c5-42c7-8f2e-1eb39f6d9125"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0bb9e56d-3c7c-43ce-8a77-f9780ef93a15", "AQAAAAEAACcQAAAAEJ90WZQAkjX6TFA4ai7TnvqCMLr06koE7o3AysOmgckynYVt56JuQO1y5DWp19TPWg==", "06ae4aa5-be7e-412c-8f6d-c14d1be2e044" });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_BlogId",
                table: "Likes",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Blogs_BlogId",
                table: "Likes",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Blogs_BlogId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_BlogId",
                table: "Likes");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c5e174e-3b0e-446f-86af-483d56fd7210"),
                column: "ConcurrencyStamp",
                value: "638554759192979774");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e943ffbf-65a4-4d42-bb74-f2ca9ea8d22a"),
                column: "ConcurrencyStamp",
                value: "638554759192979796");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8f3d96ce-76ec-4992-911a-33ceb81fa29d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a52d7b05-2838-4e35-8c0a-b06b3154cb20", "AQAAAAEAACcQAAAAEN629yJ1iXssVPi/nOYEsQPB3ZJCAvGGclZnOW66lj2HleaKe1sTS7FH4Jr1DgrfjA==", "b46e8d5a-8a80-4a1f-9e76-44bfd98a19a7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e9b3be8c-99c5-42c7-8f2e-1eb39f6d9125"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a472e8c-1a9e-4b3d-80ab-cbc79dfa432d", "AQAAAAEAACcQAAAAEOa3UAwdoGO1WCs0w1T8fP05+DUCjB425ooEjgYIGHoIM0Gi3XB22ojS+HK04DfAMQ==", "e5b65b2a-9b0d-460b-a602-a175cd217c78" });
        }
    }
}
