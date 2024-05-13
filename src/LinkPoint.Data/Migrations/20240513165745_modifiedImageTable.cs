using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkPoint.Data.Migrations
{
    /// <inheritdoc />
    public partial class modifiedImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_PostId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PostId",
                table: "Images",
                column: "PostId",
                unique: true,
                filter: "[PostId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_PostId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PostId",
                table: "Images",
                column: "PostId",
                unique: true);
        }
    }
}
