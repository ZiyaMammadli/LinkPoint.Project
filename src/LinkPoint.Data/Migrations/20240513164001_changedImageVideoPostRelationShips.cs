using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkPoint.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedImageVideoPostRelationShips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Images_ImageId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Videos_VideoId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ImageId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_VideoId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_PostId",
                table: "Videos",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PostId",
                table: "Images",
                column: "PostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Posts_PostId",
                table: "Images",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Posts_PostId",
                table: "Videos",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Posts_PostId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Posts_PostId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_PostId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Images_PostId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ImageId",
                table: "Posts",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_VideoId",
                table: "Posts",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Images_ImageId",
                table: "Posts",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Videos_VideoId",
                table: "Posts",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
