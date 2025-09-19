using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alx.Repo.Application.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Name", "UserId", "ParentId", "Description", "Domain", "Content", "AuditCreatedOn", "AuditLastUpdated", "AuditCreatedByUser", "AuditLastUpdatedByUser" },
                values: new object[] { 1, "Item 01", "fa079815-1a1a-4030-97e2-dda7c6858385", 0, "Description", "Domain", "Content", DateTime.Now, DateTime.Now, "fa079815-1a1a-4030-97e2-dda7c6858385", "fa079815-1a1a-4030-97e2-dda7c6858385" }
);

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Name", "UserId", "ParentId", "Description", "Domain", "Content", "AuditCreatedOn", "AuditLastUpdated", "AuditCreatedByUser", "AuditLastUpdatedByUser" },
                values: new object[] { 2, "Item 02", "fa079815-1a1a-4030-97e2-dda7c6858385", 0, "Description", "Domain", "Content", DateTime.Now, DateTime.Now, "fa079815-1a1a-4030-97e2-dda7c6858385", "fa079815-1a1a-4030-97e2-dda7c6858385" }
            );

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Name", "UserId", "ParentId", "Description", "Domain", "Content", "AuditCreatedOn", "AuditLastUpdated", "AuditCreatedByUser", "AuditLastUpdatedByUser" },
                values: new object[] { 3, "Item 03", "fa079815-1a1a-4030-97e2-dda7c6858385", 0, "Description", "Domain", "Content", DateTime.Now, DateTime.Now, "fa079815-1a1a-4030-97e2-dda7c6858385", "fa079815-1a1a-4030-97e2-dda7c6858385" }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
