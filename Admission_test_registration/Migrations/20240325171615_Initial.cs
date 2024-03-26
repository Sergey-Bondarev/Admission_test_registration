using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admission_test_registration.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Educators",
                columns: table => new
                {
                    EducatorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    telNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Degree = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educators", x => x.EducatorId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    telNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.UniversityId);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    CertificateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    EducatorId = table.Column<int>(type: "int", nullable: false),
                    MathGrade = table.Column<int>(type: "int", nullable: false),
                    EnglishGrade = table.Column<int>(type: "int", nullable: false),
                    HistoryGrade = table.Column<int>(type: "int", nullable: false),
                    SocialActivity = table.Column<bool>(type: "bit", nullable: false),
                    CompetitionActivity = table.Column<bool>(type: "bit", nullable: false),
                    SportActivity = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.CertificateId);
                    table.ForeignKey(
                        name: "FK_Certificates_Educators_EducatorId",
                        column: x => x.EducatorId,
                        principalTable: "Educators",
                        principalColumn: "EducatorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certificates_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamScheduleElements",
                columns: table => new
                {
                    ExamScheduleElementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityId = table.Column<int>(type: "int", nullable: false),
                    ExamTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamScheduleElements", x => x.ExamScheduleElementId);
                    table.ForeignKey(
                        name: "FK_ExamScheduleElements_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationSheets",
                columns: table => new
                {
                    RegistrationSheetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ExamScheduleElementId = table.Column<int>(type: "int", nullable: false),
                    RegistrationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationSheets", x => x.RegistrationSheetId);
                    table.ForeignKey(
                        name: "FK_RegistrationSheets_ExamScheduleElements_ExamScheduleElementId",
                        column: x => x.ExamScheduleElementId,
                        principalTable: "ExamScheduleElements",
                        principalColumn: "ExamScheduleElementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrationSheets_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_EducatorId",
                table: "Certificates",
                column: "EducatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_StudentId",
                table: "Certificates",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamScheduleElements_UniversityId",
                table: "ExamScheduleElements",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationSheets_ExamScheduleElementId",
                table: "RegistrationSheets",
                column: "ExamScheduleElementId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationSheets_StudentId",
                table: "RegistrationSheets",
                column: "StudentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "RegistrationSheets");

            migrationBuilder.DropTable(
                name: "Educators");

            migrationBuilder.DropTable(
                name: "ExamScheduleElements");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
