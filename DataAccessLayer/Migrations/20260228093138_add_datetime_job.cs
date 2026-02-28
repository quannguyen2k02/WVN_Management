using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class add_datetime_job : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedCamera_LedModel_LedModelId",
                table: "LedCamera");

            migrationBuilder.AlterColumn<int>(
                name: "LedModelId",
                table: "LedCamera",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Job",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Job",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_LedCamera_LedModel_LedModelId",
                table: "LedCamera",
                column: "LedModelId",
                principalTable: "LedModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedCamera_LedModel_LedModelId",
                table: "LedCamera");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Job");

            migrationBuilder.AlterColumn<int>(
                name: "LedModelId",
                table: "LedCamera",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LedCamera_LedModel_LedModelId",
                table: "LedCamera",
                column: "LedModelId",
                principalTable: "LedModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
