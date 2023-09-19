using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class updatetableAnimefull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AiredOn",
                table: "anime",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Episodes",
                table: "anime",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EpisodesAired",
                table: "anime",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageOriginal",
                table: "anime",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePreview",
                table: "anime",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageX48",
                table: "anime",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageX96",
                table: "anime",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Kind",
                table: "anime",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleasedOn",
                table: "anime",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "anime",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AiredOn",
                table: "anime");

            migrationBuilder.DropColumn(
                name: "Episodes",
                table: "anime");

            migrationBuilder.DropColumn(
                name: "EpisodesAired",
                table: "anime");

            migrationBuilder.DropColumn(
                name: "ImageOriginal",
                table: "anime");

            migrationBuilder.DropColumn(
                name: "ImagePreview",
                table: "anime");

            migrationBuilder.DropColumn(
                name: "ImageX48",
                table: "anime");

            migrationBuilder.DropColumn(
                name: "ImageX96",
                table: "anime");

            migrationBuilder.DropColumn(
                name: "Kind",
                table: "anime");

            migrationBuilder.DropColumn(
                name: "ReleasedOn",
                table: "anime");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "anime");
        }
    }
}
