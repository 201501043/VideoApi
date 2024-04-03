using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoApi.Migrations
{
    /// <inheritdoc />
    public partial class CreatingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoMetaData",
                columns: table => new
                {
                    VideoId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VideoTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VideoDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VideoLocation = table.Column<string>(type: "text", nullable: false),
                    ThumbnailLocation = table.Column<string>(type: "text", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isVideoUploaded = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    isVideoProcessed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VIDEOID", x => x.VideoId);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryChunkDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CurrentChunk = table.Column<int>(type: "int", nullable: false),
                    TotalChunks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMP_ID", x => x.id);
                    table.ForeignKey(
                        name: "FK_TemporaryChunkDetails_VideoMetaData_VideoId",
                        column: x => x.VideoId,
                        principalTable: "VideoMetaData",
                        principalColumn: "VideoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryChunkDetails_VideoId",
                table: "TemporaryChunkDetails",
                column: "VideoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemporaryChunkDetails");

            migrationBuilder.DropTable(
                name: "VideoMetaData");
        }
    }
}
