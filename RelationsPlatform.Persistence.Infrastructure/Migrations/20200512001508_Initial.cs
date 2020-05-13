using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DisciplinePicker.Persistence.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Faculty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Faculty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineAvailabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Semester = table.Column<int>(nullable: false),
                    Hours = table.Column<int>(nullable: false),
                    MaxAllovedStudents = table.Column<int>(nullable: false),
                    MinAllovedStudents = table.Column<int>(nullable: false),
                    DisciplineId = table.Column<Guid>(nullable: false),
                    LecturerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discipline_DisciplineAvailabilities",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lecturer_DisciplineAvailabilities",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    Course = table.Column<int>(nullable: false),
                    Speciality = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineChoises",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DisciplineAvailabilityId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineChoises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineChoise_DisciplineAvailability",
                        column: x => x.DisciplineAvailabilityId,
                        principalTable: "DisciplineAvailabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DisciplineChoise_Student",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Disciplines",
                columns: new[] { "Id", "Department", "Description", "Faculty", "Name" },
                values: new object[,]
                {
                    { new Guid("0874505b-c85f-4df0-aa4f-68069ef1e656"), "Department1", "Description1", "Faculty1", "Discipline1" },
                    { new Guid("13ff5772-8d9b-465f-9b23-654143740a09"), "Department2", "Description2", "Faculty2", "Discipline2" },
                    { new Guid("ec5554e8-b31b-40ca-ab2f-fe4cdd6a6dab"), "Department3", "Description3", "Faculty3", "Discipline3" }
                });

            migrationBuilder.InsertData(
                table: "Lecturers",
                columns: new[] { "Id", "Department", "Faculty", "Name" },
                values: new object[,]
                {
                    { new Guid("4f38f789-cae6-45a8-9806-dff3380ccd12"), "Інформаційних систем", "Прикладна математика", "Тарасюк Іван" },
                    { new Guid("9d2edad8-b371-4995-a5af-81b97ba5080b"), "Інформаційних систем", "Прикладна математика", "Ім'я Прізвище" },
                    { new Guid("25541f87-9939-43b2-b190-7440b2ab5571"), "Інформаційних систем", "Прикладна математика", "Ім'я Прізвище2" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2432fe24-3a95-4bfe-85bd-4191cd41b230"), "admin" },
                    { new Guid("442d3c5c-8737-4934-bac9-f20f35e99a88"), "student" }
                });

            migrationBuilder.InsertData(
                table: "DisciplineAvailabilities",
                columns: new[] { "Id", "DisciplineId", "Hours", "LecturerId", "MaxAllovedStudents", "MinAllovedStudents", "Semester", "Year" },
                values: new object[,]
                {
                    { new Guid("9814c8b5-935a-4e8b-9b2e-8d5341f94a4d"), new Guid("13ff5772-8d9b-465f-9b23-654143740a09"), 90, new Guid("4f38f789-cae6-45a8-9806-dff3380ccd12"), 200, 25, 5, 2020 },
                    { new Guid("58e61323-d9ba-4190-a5aa-82ef2b57f25b"), new Guid("0874505b-c85f-4df0-aa4f-68069ef1e656"), 90, new Guid("9d2edad8-b371-4995-a5af-81b97ba5080b"), 200, 25, 7, 2020 },
                    { new Guid("08124a13-c3db-4dfe-8760-b084d3b2ed49"), new Guid("0874505b-c85f-4df0-aa4f-68069ef1e656"), 90, new Guid("25541f87-9939-43b2-b190-7440b2ab5571"), 200, 25, 6, 2020 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Course", "Department", "Group", "Login", "Name", "Password", "Photo", "RoleId", "Speciality" },
                values: new object[,]
                {
                    { new Guid("1f444245-51e5-452a-bdab-6010e82b8937"), 0, null, null, "admin.com", null, "123", null, new Guid("2432fe24-3a95-4bfe-85bd-4191cd41b230"), null },
                    { new Guid("3b4e5908-7842-4523-9ea6-bb14befb3d0e"), 3, "Інформаційних систем", "Gp37", "bozhook@gmail.com", "Віктор Божук", "123456aA_", null, new Guid("442d3c5c-8737-4934-bac9-f20f35e99a88"), "Комп'ютерні науки" },
                    { new Guid("8b827de2-61a0-42ed-81b5-7360aad5664f"), 3, "Інформаційних систем", "Gp38", "B@gmail.com", "Власюк Василь", "123456aA_", null, new Guid("442d3c5c-8737-4934-bac9-f20f35e99a88"), "Комп'ютерні науки" },
                    { new Guid("2a2b6f7b-7084-4241-86da-fe19cbb39d4c"), 3, "Інформаційних систем", "Gp39", "M@gmail.com", "Щерба Максим", "123456aA_", null, new Guid("442d3c5c-8737-4934-bac9-f20f35e99a88"), "Комп'ютерні науки" },
                    { new Guid("d8ed0233-40a4-44a1-85a6-8191c4a99683"), 2, "Інформаційних систем нейронних", "Gp40", "А@gmail.com", "Альфа Дельта", "1234", null, new Guid("442d3c5c-8737-4934-bac9-f20f35e99a88"), "Комп'ютерні науки богарта" },
                    { new Guid("3392ed82-0a7a-42db-b50d-61e1b0b1380d"), 1, "Риторичних систем нейронних", "Gp42", "В@gmail.com", "Браво Фокстрот", "1234", null, new Guid("442d3c5c-8737-4934-bac9-f20f35e99a88"), "Міщанські науки" },
                    { new Guid("0a6808c6-f0b5-4a27-95d3-8f1c00f76c99"), 3, "Інформаційних підсистем", "Gp50", "А@gmail.com", "Лютер Кінг", "1234", null, new Guid("442d3c5c-8737-4934-bac9-f20f35e99a88"), "Комп'ютерні монітори" }
                });

            migrationBuilder.InsertData(
                table: "DisciplineChoises",
                columns: new[] { "Id", "DisciplineAvailabilityId", "StudentId" },
                values: new object[] { new Guid("ed10cc99-2f36-47b1-a4f2-4b48860011e4"), new Guid("08124a13-c3db-4dfe-8760-b084d3b2ed49"), new Guid("3b4e5908-7842-4523-9ea6-bb14befb3d0e") });

            migrationBuilder.InsertData(
                table: "DisciplineChoises",
                columns: new[] { "Id", "DisciplineAvailabilityId", "StudentId" },
                values: new object[] { new Guid("10b8730d-5d7e-4b35-9c92-2c08cc98d0af"), new Guid("58e61323-d9ba-4190-a5aa-82ef2b57f25b"), new Guid("3b4e5908-7842-4523-9ea6-bb14befb3d0e") });

            migrationBuilder.InsertData(
                table: "DisciplineChoises",
                columns: new[] { "Id", "DisciplineAvailabilityId", "StudentId" },
                values: new object[] { new Guid("c968e07a-d89a-426b-afa6-cb6e761bdfd6"), new Guid("08124a13-c3db-4dfe-8760-b084d3b2ed49"), new Guid("8b827de2-61a0-42ed-81b5-7360aad5664f") });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineAvailabilities_DisciplineId",
                table: "DisciplineAvailabilities",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineAvailabilities_LecturerId",
                table: "DisciplineAvailabilities",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineChoises_DisciplineAvailabilityId",
                table: "DisciplineChoises",
                column: "DisciplineAvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineChoises_StudentId",
                table: "DisciplineChoises",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_RoleId",
                table: "Students",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisciplineChoises");

            migrationBuilder.DropTable(
                name: "DisciplineAvailabilities");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
