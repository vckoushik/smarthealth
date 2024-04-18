﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smarthealth.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMedicalRecord2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_UserId1",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_UserId1",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "MedicalRecords");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MedicalRecords",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_UserId",
                table: "MedicalRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_UserId",
                table: "MedicalRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_UserId",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_UserId",
                table: "MedicalRecords");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MedicalRecords",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "MedicalRecords",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_UserId1",
                table: "MedicalRecords",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_UserId1",
                table: "MedicalRecords",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
