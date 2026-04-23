using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loqim.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddAiRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessProfiles_tenants_TenantId",
                table: "BusinessProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessProfiles",
                table: "BusinessProfiles");

            migrationBuilder.DropIndex(
                name: "IX_BusinessProfiles_TenantId",
                table: "BusinessProfiles");

            migrationBuilder.RenameTable(
                name: "BusinessProfiles",
                newName: "business_profiles");

            migrationBuilder.AlterColumn<string>(
                name: "ToneOfVoice",
                table: "business_profiles",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Segment",
                table: "business_profiles",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "MainGoal",
                table: "business_profiles",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "business_profiles",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessHours",
                table: "business_profiles",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_business_profiles",
                table: "business_profiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ai_rules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ai_rules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ai_rules_tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_business_profiles_TenantId",
                table: "business_profiles",
                column: "TenantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ai_rules_TenantId",
                table: "ai_rules",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_business_profiles_tenants_TenantId",
                table: "business_profiles",
                column: "TenantId",
                principalTable: "tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_business_profiles_tenants_TenantId",
                table: "business_profiles");

            migrationBuilder.DropTable(
                name: "ai_rules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_business_profiles",
                table: "business_profiles");

            migrationBuilder.DropIndex(
                name: "IX_business_profiles_TenantId",
                table: "business_profiles");

            migrationBuilder.RenameTable(
                name: "business_profiles",
                newName: "BusinessProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "ToneOfVoice",
                table: "BusinessProfiles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Segment",
                table: "BusinessProfiles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "MainGoal",
                table: "BusinessProfiles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BusinessProfiles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "BusinessHours",
                table: "BusinessProfiles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessProfiles",
                table: "BusinessProfiles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProfiles_TenantId",
                table: "BusinessProfiles",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessProfiles_tenants_TenantId",
                table: "BusinessProfiles",
                column: "TenantId",
                principalTable: "tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
