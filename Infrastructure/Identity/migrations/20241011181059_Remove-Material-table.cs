using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Identity.migrations
{
    /// <inheritdoc />
    public partial class RemoveMaterialtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Payment_PaymentId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Progresses_CourseMaterials_MaterialId",
                table: "Progresses");

            migrationBuilder.DropTable(
                name: "CourseMaterials");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Progresses",
                table: "Progresses");

            migrationBuilder.DropIndex(
                name: "IX_Progresses_EnrollmentId",
                table: "Progresses");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_PaymentId",
                table: "Enrollments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a2a5fa4-37bd-440a-b592-f0bb8aeb4f72");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c7ae234-a320-416f-b983-0a295ce5f2a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf135e10-4bbc-4a65-88bc-9f3705659d3e");

            migrationBuilder.DropColumn(
                name: "ProgressId",
                table: "Progresses");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "LastMaterialSequence",
                table: "CoursesSections");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "Progresses",
                newName: "SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Progresses_MaterialId",
                table: "Progresses",
                newName: "IX_Progresses_SectionId");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "CoursesSections",
                type: "NVARCHAR(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Progresses",
                table: "Progresses",
                columns: new[] { "EnrollmentId", "SectionId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e94f545-b03d-439f-8aa0-65719b8e663e", null, "Admin", "ADMIN" },
                    { "67b51ef3-e989-46eb-b492-133a93910c72", null, "Instructor", "Instructor" },
                    { "861dceeb-74d2-41d7-96ff-a02b33879136", null, "Student", "STUDENT" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Progresses_CoursesSections_SectionId",
                table: "Progresses",
                column: "SectionId",
                principalTable: "CoursesSections",
                principalColumn: "SectionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progresses_CoursesSections_SectionId",
                table: "Progresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Progresses",
                table: "Progresses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e94f545-b03d-439f-8aa0-65719b8e663e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67b51ef3-e989-46eb-b492-133a93910c72");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "861dceeb-74d2-41d7-96ff-a02b33879136");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "CoursesSections");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "Progresses",
                newName: "MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Progresses_SectionId",
                table: "Progresses",
                newName: "IX_Progresses_MaterialId");

            migrationBuilder.AddColumn<int>(
                name: "ProgressId",
                table: "Progresses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "Enrollments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "LastMaterialSequence",
                table: "CoursesSections",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Progresses",
                table: "Progresses",
                column: "ProgressId");

            migrationBuilder.CreateTable(
                name: "CourseMaterials",
                columns: table => new
                {
                    MaterialID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SectionID = table.Column<Guid>(type: "TEXT", nullable: false),
                    CourseId = table.Column<Guid>(type: "TEXT", nullable: true),
                    MaterialSequence = table.Column<int>(type: "INTEGER", nullable: false),
                    MaterialType = table.Column<int>(type: "INT", nullable: false),
                    TextContent = table.Column<string>(type: "NVARCHAR(255)", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMaterials", x => x.MaterialID);
                    table.ForeignKey(
                        name: "FK_CourseMaterials_CoursesSections_SectionID",
                        column: x => x.SectionID,
                        principalTable: "CoursesSections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseMaterials_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseID");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Amount = table.Column<double>(type: "REAL", maxLength: 50, nullable: false),
                    CourseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Discount = table.Column<double>(type: "REAL", nullable: true),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    paymentStatus = table.Column<int>(type: "INTEGER", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a2a5fa4-37bd-440a-b592-f0bb8aeb4f72", null, "Student", "STUDENT" },
                    { "4c7ae234-a320-416f-b983-0a295ce5f2a4", null, "Admin", "ADMIN" },
                    { "cf135e10-4bbc-4a65-88bc-9f3705659d3e", null, "Instructor", "Instructor" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_EnrollmentId",
                table: "Progresses",
                column: "EnrollmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_PaymentId",
                table: "Enrollments",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMaterials_CourseId",
                table: "CourseMaterials",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMaterials_SectionID",
                table: "CourseMaterials",
                column: "SectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Payment_PaymentId",
                table: "Enrollments",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Progresses_CourseMaterials_MaterialId",
                table: "Progresses",
                column: "MaterialId",
                principalTable: "CourseMaterials",
                principalColumn: "MaterialID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
