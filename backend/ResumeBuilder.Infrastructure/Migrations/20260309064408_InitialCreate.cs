using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeBuilder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    JobTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ProfessionalSummary = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Skills = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Projects = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    Qualifications = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    References = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_Email",
                table: "Resumes",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_JobTitle",
                table: "Resumes",
                column: "JobTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resumes");
        }
    }
}
