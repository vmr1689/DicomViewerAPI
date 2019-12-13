using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DicomViewerAPI.Migrations
{
    public partial class NotesInstanceModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InstanceId",
                table: "Notes",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "InstanceId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
