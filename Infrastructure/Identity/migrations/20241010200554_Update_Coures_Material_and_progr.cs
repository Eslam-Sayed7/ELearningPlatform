using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Identity.migrations
{
    /// <inheritdoc />
    public partial class Update_Coures_Material_and_progr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Content_Courses_CourseID",
                table: "Course_Content");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Materials_Courses_CourseID",
                table: "Course_Materials");

            migrationBuilder.DropTable(
                name: "TextContents");

            migrationBuilder.DropTable(
                name: "Video_Contents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course_Materials",
                table: "Course_Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course_Content",
                table: "Course_Content");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9495bdab-8689-4b2f-b820-9c94af44279c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d74fc404-e8ea-4161-8bbd-8546be8e05f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8f8bc68-ba4e-421a-be1a-f12f9f03353f");

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Course_Materials");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Course_Content");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "Course_Content");

            migrationBuilder.DropColumn(
                name: "level",
                table: "Course_Content");

            migrationBuilder.RenameTable(
                name: "Course_Materials",
                newName: "CourseMaterials");

            migrationBuilder.RenameTable(
                name: "Course_Content",
                newName: "CoursesSections");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "CourseMaterials",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "Sequence",
                table: "CourseMaterials",
                newName: "MaterialSequence");

            migrationBuilder.RenameIndex(
                name: "IX_Course_Materials_CourseID",
                table: "CourseMaterials",
                newName: "IX_CourseMaterials_CourseId");

            migrationBuilder.RenameColumn(
                name: "ContentID",
                table: "CoursesSections",
                newName: "SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_Content_CourseID",
                table: "CoursesSections",
                newName: "IX_CoursesSections_CourseID");

            migrationBuilder.AddColumn<double>(
                name: "ProgressPercentage",
                table: "Enrollments",
                type: "REAL",
                nullable: true,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "LastSectionSequence",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "CourseMaterials",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "MaterialID",
                table: "CourseMaterials",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialType",
                table: "CourseMaterials",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SectionID",
                table: "CourseMaterials",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TextContent",
                table: "CourseMaterials",
                type: "NVARCHAR(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "CourseMaterials",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "CoursesSections",
                type: "NVARCHAR(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastMaterialSequence",
                table: "CoursesSections",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SectionSequence",
                table: "CoursesSections",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseMaterials",
                table: "CourseMaterials",
                column: "MaterialID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesSections",
                table: "CoursesSections",
                column: "SectionId");

            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    ProgressId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EnrollmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MaterialId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progresses", x => x.ProgressId);
                    table.ForeignKey(
                        name: "FK_Progresses_CourseMaterials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "CourseMaterials",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Progresses_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "EnrollmentId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_CourseMaterials_SectionID",
                table: "CourseMaterials",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_EnrollmentId",
                table: "Progresses",
                column: "EnrollmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_MaterialId",
                table: "Progresses",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMaterials_CoursesSections_SectionID",
                table: "CourseMaterials",
                column: "SectionID",
                principalTable: "CoursesSections",
                principalColumn: "SectionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMaterials_Courses_CourseId",
                table: "CourseMaterials",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesSections_Courses_CourseID",
                table: "CoursesSections",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseMaterials_CoursesSections_SectionID",
                table: "CourseMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseMaterials_Courses_CourseId",
                table: "CourseMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesSections_Courses_CourseID",
                table: "CoursesSections");

            migrationBuilder.DropTable(
                name: "Progresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesSections",
                table: "CoursesSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseMaterials",
                table: "CourseMaterials");

            migrationBuilder.DropIndex(
                name: "IX_CourseMaterials_SectionID",
                table: "CourseMaterials");

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
                name: "ProgressPercentage",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "LastSectionSequence",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LastMaterialSequence",
                table: "CoursesSections");

            migrationBuilder.DropColumn(
                name: "SectionSequence",
                table: "CoursesSections");

            migrationBuilder.DropColumn(
                name: "MaterialType",
                table: "CourseMaterials");

            migrationBuilder.DropColumn(
                name: "SectionID",
                table: "CourseMaterials");

            migrationBuilder.DropColumn(
                name: "TextContent",
                table: "CourseMaterials");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "CourseMaterials");

            migrationBuilder.RenameTable(
                name: "CoursesSections",
                newName: "Course_Content");

            migrationBuilder.RenameTable(
                name: "CourseMaterials",
                newName: "Course_Materials");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "Course_Content",
                newName: "ContentID");

            migrationBuilder.RenameIndex(
                name: "IX_CoursesSections_CourseID",
                table: "Course_Content",
                newName: "IX_Course_Content_CourseID");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Course_Materials",
                newName: "CourseID");

            migrationBuilder.RenameColumn(
                name: "MaterialSequence",
                table: "Course_Materials",
                newName: "Sequence");

            migrationBuilder.RenameIndex(
                name: "IX_CourseMaterials_CourseId",
                table: "Course_Materials",
                newName: "IX_Course_Materials_CourseID");

            migrationBuilder.AddColumn<double>(
                name: "Progress",
                table: "Enrollments",
                type: "REAL",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Course_Content",
                type: "VARCHAR(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(255)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Course_Content",
                type: "DATETIME",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "Course_Content",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "level",
                table: "Course_Content",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseID",
                table: "Course_Materials",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaterialID",
                table: "Course_Materials",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Course_Materials",
                type: "VARCHAR(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course_Content",
                table: "Course_Content",
                column: "ContentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course_Materials",
                table: "Course_Materials",
                column: "MaterialID");

            migrationBuilder.CreateTable(
                name: "TextContents",
                columns: table => new
                {
                    TextID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContentID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    Sequence = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextContents", x => x.TextID);
                    table.ForeignKey(
                        name: "FK_TextContents_Course_Content_ContentID",
                        column: x => x.ContentID,
                        principalTable: "Course_Content",
                        principalColumn: "ContentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Video_Contents",
                columns: table => new
                {
                    VideoID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContentID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Duration = table.Column<double>(type: "REAL", nullable: false),
                    Sequence = table.Column<int>(type: "INTEGER", nullable: false),
                    VideoURL = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video_Contents", x => x.VideoID);
                    table.ForeignKey(
                        name: "FK_Video_Contents_Course_Content_ContentID",
                        column: x => x.ContentID,
                        principalTable: "Course_Content",
                        principalColumn: "ContentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9495bdab-8689-4b2f-b820-9c94af44279c", null, "Instructor", "Instructor" },
                    { "d74fc404-e8ea-4161-8bbd-8546be8e05f9", null, "Admin", "ADMIN" },
                    { "f8f8bc68-ba4e-421a-be1a-f12f9f03353f", null, "Student", "STUDENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextContents_ContentID",
                table: "TextContents",
                column: "ContentID");

            migrationBuilder.CreateIndex(
                name: "IX_Video_Contents_ContentID",
                table: "Video_Contents",
                column: "ContentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Content_Courses_CourseID",
                table: "Course_Content",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Materials_Courses_CourseID",
                table: "Course_Materials",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
