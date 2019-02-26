using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TherapyAPI.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AppointmentTypeId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    TherapistId = table.Column<Guid>(nullable: false),
                    AppointmentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Gender = table.Column<char>(nullable: false),
                    MobileNumber = table.Column<string>(maxLength: 9, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    CivilStatus = table.Column<char>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    NIF = table.Column<string>(maxLength: 9, nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    Observations = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Therapists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Gender = table.Column<char>(nullable: false),
                    MobileNumber = table.Column<string>(maxLength: 9, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    PreferLanguage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AppointmentId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Billings_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("d5082063-1408-487e-9016-aebda09f77ad"), "FLORAIS", "Florais" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("a7eb6971-0c90-453d-97ab-a1d546dd8e0b"), "ACUPUNTURA", "Acupuntura" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("0d377695-87f0-4461-aaed-c3eb48eeb43d"), "CROMOTERAPIA", "Cromoterapia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("20f2d903-59ca-4334-9453-576b810230ff"), "MASSAGEM", "Massagem" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("0ec4fc11-799b-4912-ba5c-5a30856fb236"), "TERAPIACOMFLORES", "Terapia Com Flores" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("c79e2d78-5436-4b42-a3ed-3018214019bf"), "FITOTERAPIA", "Fitoterapia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("059adb02-1a31-4283-96f8-05085d744f8d"), "REFLEXOLOGIA", "Reflexologia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("f8d7306e-a63c-426a-bfd2-a405b26e2316"), "SHIATSU", "Shiatsu" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("29fa3162-9cf7-46f7-b988-927dc0a29171"), "REIKI", "Reiki" });

            migrationBuilder.CreateIndex(
                name: "IX_Billings_AppointmentId",
                table: "Billings",
                column: "AppointmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentType");

            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Therapists");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
