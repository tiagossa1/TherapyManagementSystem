using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TherapyAPI.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    AppointmentTypeId = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    TherapistId = table.Column<Guid>(nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentTypes");

            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Therapists");
        }
    }
}
