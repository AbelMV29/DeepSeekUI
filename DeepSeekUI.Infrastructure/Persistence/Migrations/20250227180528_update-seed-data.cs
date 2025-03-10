using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeepSeekUI.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6bffdbac-5fd2-4ca4-a744-2ebd6bc8efd9"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("f64db436-cfe2-4c5e-8b04-71e91ece492a"), 0, "f4e43372-a268-4f14-aef8-e9152513f432", "user@mail.com", false, false, null, "USER@MAIL.COM", "USUARIO PRUEBA", "AQAAAAIAAYagAAAAEJo4rZWt7at+1NLmAgN4AHiNoKUomJR20H97920bdycSpNW1BSAOD9dOmMBJK3jJbg==", null, false, "b77e0019-e13a-46ab-a48f-f905d3a4854c", false, "Usuario prueba" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f64db436-cfe2-4c5e-8b04-71e91ece492a"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("6bffdbac-5fd2-4ca4-a744-2ebd6bc8efd9"), 0, "cb61e814-32f0-45d6-820f-7b7d3bd842af", "user@mail.com", false, false, null, "USER@MAIL.COM", "USUARIO PRUEBA", "AQAAAAIAAYagAAAAEMJ3TdzayhTb3brn+FsteINGu+SvLqPbkreZJvaYgI9SQrZJ7ewS/tAV8voJyhsY6g==", null, false, null, false, "Usuario prueba" });
        }
    }
}
