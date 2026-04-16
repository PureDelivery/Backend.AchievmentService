using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PureDelivery.Achievments.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AchievementDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EventCriteria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CalculationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TargetValue = table.Column<double>(type: "float", nullable: false),
                    LoyaltyPointsReward = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AchievementUserProgress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AchievmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementUserProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AchievementUserProgress_AchievementDefinitions_AchievmentId",
                        column: x => x.AchievmentId,
                        principalTable: "AchievementDefinitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserAchievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AchievmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAchievements_AchievementDefinitions_AchievmentId",
                        column: x => x.AchievmentId,
                        principalTable: "AchievementDefinitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AchievementDefinitions_EventCriteria",
                table: "AchievementDefinitions",
                column: "EventCriteria");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementUserProgress_AchievmentId",
                table: "AchievementUserProgress",
                column: "AchievmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementUserProgress_UserId_AchievmentId",
                table: "AchievementUserProgress",
                columns: new[] { "UserId", "AchievmentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_AchievmentId",
                table: "UserAchievements",
                column: "AchievmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_UserId",
                table: "UserAchievements",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AchievementUserProgress");

            migrationBuilder.DropTable(
                name: "UserAchievements");

            migrationBuilder.DropTable(
                name: "AchievementDefinitions");
        }
    }
}
