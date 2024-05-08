using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Point.Data.Migrations
{
    public partial class CreatingDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            //migrationBuilder.CreateTable(
            //    name: "AspNetUsers",
            //    schema: "Identity",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PointType = table.Column<int>(type: "int", nullable: false),
                    DateHour = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Points");
        }
    }
}
