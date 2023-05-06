using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportationAPI.Migrations
{
    /// <inheritdoc />
    public partial class RollesSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString() }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From [Role]");
        }
    }
}
