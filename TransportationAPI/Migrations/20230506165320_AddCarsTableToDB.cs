using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportationAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCarsTableToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MoterNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FramNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Kind = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RenewalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
