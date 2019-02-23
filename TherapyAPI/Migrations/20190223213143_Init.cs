using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TherapyAPI.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    TherapistId = table.Column<Guid>(nullable: false),
                    AppointmentId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_Appointments_AppointmentType_AppointmentTypeId",
                        column: x => x.AppointmentTypeId,
                        principalTable: "AppointmentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Therapists_TherapistId",
                        column: x => x.TherapistId,
                        principalTable: "Therapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("d5165995-4328-4ade-a0c8-26b522ce39c0"), "FLORAIS", "Florais" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("513a61b2-884e-44d8-b47b-69e535802dd8"), "ACUPUNTURA", "Acupuntura" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("73f80f13-75b0-48e0-874d-a43e2e444485"), "CROMOTERAPIA", "Cromoterapia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("a07da813-d2b5-43e3-a995-28fb26836658"), "MASSAGEM", "Massagem" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("bce75384-b900-452f-a60a-0298751a4f2f"), "TERAPIACOMFLORES", "Terapia Com Flores" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("9826cede-c81c-4662-a0d5-cacb1d898b50"), "FITOTERAPIA", "Fitoterapia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("2296edb8-09ab-4dd7-8b07-2e4b4a9cd118"), "REFLEXOLOGIA", "Reflexologia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("5859f090-a801-4a16-ab88-a291f8bae425"), "SHIATSU", "Shiatsu" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("1aabf68d-d1c9-41a1-b1f2-bf12b4d7e62b"), "REIKI", "Reiki" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentTypeId",
                table: "Appointments",
                column: "AppointmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TherapistId",
                table: "Appointments",
                column: "TherapistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.DropTable(
                name: "AppointmentType");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Therapists");
        }
    }
}
