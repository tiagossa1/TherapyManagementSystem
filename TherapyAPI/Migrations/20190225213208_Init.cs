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

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("0209baaa-cc0d-47f6-8d79-0ee02b6a62e0"), "FLORAIS", "Florais" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("c8014002-c739-4676-be98-e16f4170654c"), "ACUPUNTURA", "Acupuntura" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("42e0cf8b-f140-46d8-85eb-b395eec574de"), "CROMOTERAPIA", "Cromoterapia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("9bd2fa11-d0e9-4fdf-84d3-9a9e72d50585"), "MASSAGEM", "Massagem" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("4e257c5a-12fb-4d75-b1f1-93ddb9453582"), "TERAPIACOMFLORES", "Terapia Com Flores" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("993375f1-4503-437e-9402-3edbb311b46d"), "FITOTERAPIA", "Fitoterapia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("0784c86a-78d0-4883-8b31-3b2adf943b8c"), "REFLEXOLOGIA", "Reflexologia" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("354ed255-443b-4ca4-96d2-250b625e503f"), "SHIATSU", "Shiatsu" });

            migrationBuilder.InsertData(
                table: "AppointmentType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("71822333-5a68-47cc-97c3-9d59ead8b495"), "REIKI", "Reiki" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

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
        }
    }
}
