using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuditTrailSample.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_PropertyName_To_Audit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                table: "Audits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertyName",
                table: "Audits");
        }
    }
}
