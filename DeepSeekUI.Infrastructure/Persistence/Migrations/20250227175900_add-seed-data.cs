using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeepSeekUI.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("6bffdbac-5fd2-4ca4-a744-2ebd6bc8efd9"), 0, "cb61e814-32f0-45d6-820f-7b7d3bd842af", "user@mail.com", false, false, null, "USER@MAIL.COM", "USUARIO PRUEBA", "AQAAAAIAAYagAAAAEMJ3TdzayhTb3brn+FsteINGu+SvLqPbkreZJvaYgI9SQrZJ7ewS/tAV8voJyhsY6g==", null, false, null, false, "Usuario prueba" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6bffdbac-5fd2-4ca4-a744-2ebd6bc8efd9"));
        }
    }
}
