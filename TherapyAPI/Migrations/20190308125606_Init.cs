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
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Gender = table.Column<char>(nullable: false),
                    MobileNumber = table.Column<string>(maxLength: 9, nullable: true),
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
                    MobileNumber = table.Column<string>(maxLength: 9, nullable: true),
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
                    AppointmentDate = table.Column<DateTime>(nullable: false),
                    AppointmentTime = table.Column<TimeSpan>(nullable: false)
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
                values: new object[] { new Guid("ba1a5585-f976-4016-af8b-000ebfede7bb"), "FLORAIS", "Florais" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("601d8a79-759d-4227-af7c-93165ef10bc5"), "ACUPUNTURA", "Acupuntura" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("ba8629cb-0c4d-4143-9380-04387883a6e3"), "CROMOTERAPIA", "Cromoterapia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("6b0a9cf8-3ada-4b5b-b66b-c042ecbde5c2"), "MASSAGEM", "Massagem" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("dd2d567b-0fba-4164-9cd4-d8399763e82c"), "TERAPIACOMFLORES", "Terapia Com Flores" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("7fac5816-5eb6-4eb2-990d-62f55ac2f9ae"), "FITOTERAPIA", "Fitoterapia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("189097c1-65e3-4079-bc78-45df0e219fb9"), "REFLEXOLOGIA", "Reflexologia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("e25193ca-760c-49d5-8b4e-0c71d90c2fdc"), "SHIATSU", "Shiatsu" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("4a002c74-7f63-48bf-9883-4a6d78b353ef"), "REIKI", "Reiki" });

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

            migrationBuilder.CreateIndex(
                name: "IX_Billings_AppointmentId",
                table: "Billings",
                column: "AppointmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AppointmentType");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Therapists");
        }
    }
}
