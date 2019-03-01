using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TherapyAPI.Migrations
{
    public partial class init : Migration
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
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AppointmentId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Discount = table.Column<bool>(nullable: false)
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
                values: new object[] { new Guid("9e6dcae1-f0f4-4fb2-953e-09c6f6760270"), "FLORAIS", "Florais" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("5310e626-e8bf-4ed9-bb5c-da2a0d32f00c"), "ACUPUNTURA", "Acupuntura" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("cb9bc9ad-8c7b-419a-9b42-f13307dd459c"), "CROMOTERAPIA", "Cromoterapia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("97f79615-6ca9-4c82-9655-8cc952eb4aa3"), "MASSAGEM", "Massagem" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("b35a10c3-0d43-4dc5-b1d4-a591fdd36b94"), "TERAPIACOMFLORES", "Terapia Com Flores" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("bec6ac16-7932-48dc-903f-4f6512f1f1fb"), "FITOTERAPIA", "Fitoterapia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("7d593a97-370c-424b-a6d2-7a783841ba6a"), "REFLEXOLOGIA", "Reflexologia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("ca3dac30-56b5-4a2e-9549-095d007fa436"), "SHIATSU", "Shiatsu" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("50593c90-2d39-4acc-bd78-a9804ef32237"), "REIKI", "Reiki" });

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
                name: "Appointments");
        }
    }
}
