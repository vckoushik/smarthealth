using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smarthealth.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMedicalRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URL",
                table: "MedicalRecords",
                newName: "FileName");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "MedicalRecords",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileData",
                table: "MedicalRecords");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "MedicalRecords",
                newName: "URL");
        }
    }
}
